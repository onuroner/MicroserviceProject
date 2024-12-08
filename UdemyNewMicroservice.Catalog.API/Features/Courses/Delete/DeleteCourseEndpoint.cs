using AutoMapper;
using MediatR;
using MicroserviceProject.Shared;
using MicroserviceProject.Shared.Extensions;
using UdemyNewMicroservice.Catalog.API.Features.Courses.Dto;
using UdemyNewMicroservice.Catalog.API.Repositories;

namespace UdemyNewMicroservice.Catalog.API.Features.Courses.Delete
{
    public record DeleteCourseCommand(Guid Id) : IRequest<ServiceResult>;

    public class DeleteCourseCommandHandler(AppDbContext context) : IRequestHandler<DeleteCourseCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var hasCourse = await context.Courses.FindAsync(request.Id, cancellationToken);
            if (hasCourse is null)
            {
                return ServiceResult<CourseDto>.Error("Course does not exists", System.Net.HttpStatusCode.NotFound);
            }

            context.Courses.Remove(hasCourse);
            await context.SaveChangesAsync();
            return ServiceResult.SucccessAsNocontent();
        }
    }

    public static class DeleteCourseEndpoint
    {
        public static RouteGroupBuilder DeleteCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var result = await mediator.Send(new DeleteCourseCommand(id));
                return result.ToGenericResult();
            }).WithName("DeleteCourse");

            return group;
        }
    }
}
