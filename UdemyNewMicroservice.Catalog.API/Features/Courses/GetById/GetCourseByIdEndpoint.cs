using AutoMapper;
using MediatR;
using MicroserviceProject.Shared;
using MicroserviceProject.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using UdemyNewMicroservice.Catalog.API.Features.Categories.Dto;
using UdemyNewMicroservice.Catalog.API.Features.Courses.Dto;
using UdemyNewMicroservice.Catalog.API.Repositories;

namespace UdemyNewMicroservice.Catalog.API.Features.Courses.GetById
{
    public record GetCourseByIdQuery(Guid Id) : IRequest<ServiceResult<CourseDto>>;

    public class GetCourseByIdQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetCourseByIdQuery, ServiceResult<CourseDto>>
    {
        public async Task<ServiceResult<CourseDto>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            var hasCourse = await context.Courses.FindAsync(request.Id, cancellationToken);
            if (hasCourse is null)
            {
                return ServiceResult<CourseDto>.Error("Course does not exists", System.Net.HttpStatusCode.NotFound);
            }
            var category = await context.Categories.FindAsync(hasCourse.CategoryId, cancellationToken);
            hasCourse.Category = category;
            var mappedCourse = mapper.Map<CourseDto>(hasCourse);

            return ServiceResult<CourseDto>.SucccessAsOk(mappedCourse);
        }
    }

    public static class GetCourseByIdEndpoint
    {
        public static RouteGroupBuilder GetCourseByIdGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var result = await mediator.Send(new GetCourseByIdQuery(id));
                return result.ToGenericResult();
            }).MapToApiVersion(1, 0).WithName("GetCourseById");

            return group;
        }
    }
}
