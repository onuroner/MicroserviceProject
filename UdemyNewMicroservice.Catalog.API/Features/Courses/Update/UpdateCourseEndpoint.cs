using AutoMapper;
using FluentValidation;
using MediatR;
using MicroserviceProject.Shared;
using MicroserviceProject.Shared.Extensions;
using MicroserviceProject.Shared.Filters;
using UdemyNewMicroservice.Catalog.API.Features.Courses.Create;
using UdemyNewMicroservice.Catalog.API.Features.Courses.Dto;
using UdemyNewMicroservice.Catalog.API.Features.Courses.GetById;
using UdemyNewMicroservice.Catalog.API.Repositories;

namespace UdemyNewMicroservice.Catalog.API.Features.Courses.Update
{
    public record UpdateCourseCommand(Guid Id, string Name, String Description, decimal Price, Guid CategoryId, string? ImageUrl) : IRequest<ServiceResult>;

    public class UpdateCourseCommandHandler(AppDbContext context, IMapper mapper) : IRequestHandler<UpdateCourseCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var hasCourse = await context.Courses.FindAsync(request.Id, cancellationToken);
            if (hasCourse is null)
            {
                return ServiceResult<CourseDto>.Error("Course does not exists", System.Net.HttpStatusCode.NotFound);
            }
            hasCourse.Name = request.Name;
            hasCourse.Description = request.Description;
            hasCourse.Price = request.Price;
            hasCourse.ImageUrl = request.ImageUrl;
            hasCourse.CategoryId = request.CategoryId;

            context.Courses.Update(hasCourse);
            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult<CourseDto>.SucccessAsNocontent();
        }
    }

    public class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
    {
        public UpdateCourseCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("{PropertyName} cannot be empty.").MaximumLength(100).WithMessage("{PropertyName} must be max 100 characters.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("{PropertyName} cannot be empty.").MaximumLength(100).WithMessage("{PropertyName} must be max 1000 characters.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("{PropertyName} must be greater than zero.");

            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("{PropertyName} cannot be empty.");

        }
    }

    public static class UpdateCourseEndpoint
    {
        public static RouteGroupBuilder UpdateCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("/", async (UpdateCourseCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return result.ToGenericResult();
            }).WithName("UpdateCourse")
            .AddEndpointFilter<ValidationFilter<UpdateCourseCommand>>();

            return group;
        }
    }
}
