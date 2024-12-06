using MediatR;
using MicroserviceProject.Shared.Extensions;

namespace UdemyNewMicroservice.Catalog.API.Features.Categories.Create
{
    public static class CreateCategoryEndpoint
    {
        public static RouteGroupBuilder CreateCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateCategoryCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return result.ToGenericResult();
            });

            return group;
        }
    }
}
