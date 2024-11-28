using MediatR;
using MicroserviceProject.Shared;

namespace UdemyNewMicroservice.Catalog.API.Features.Categories.Create
{
    public record CreateCategoryCommand(string Name) : IRequest<ServiceResult<CreateCategoryResponse>>
    {
    }
}
