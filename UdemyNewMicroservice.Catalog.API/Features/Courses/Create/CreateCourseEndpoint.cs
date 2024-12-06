using MediatR;
using MicroserviceProject.Shared.Extensions;
using MicroserviceProject.Shared.Filters;

namespace UdemyNewMicroservice.Catalog.API.Features.Courses.Create
{
    public static class CreateCourseEndpoint
    {
        public static RouteGroupBuilder CreateCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateCourseCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return result.ToGenericResult();
            }).WithName("CreateCourse")
            .Produces<Guid>(StatusCodes.Status201Created)
            .AddEndpointFilter<ValidationFilter<CreateCourseCommand>>();

            return group;
        }
    }
}
