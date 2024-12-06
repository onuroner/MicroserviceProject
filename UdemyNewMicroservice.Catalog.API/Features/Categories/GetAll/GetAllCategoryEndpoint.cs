using MassTransit;
using MediatR;
using MicroserviceProject.Shared;
using MicroserviceProject.Shared.Extensions;
using MicroserviceProject.Shared.Filters;
using Microsoft.EntityFrameworkCore;
using UdemyNewMicroservice.Catalog.API.Features.Categories.Create;
using UdemyNewMicroservice.Catalog.API.Features.Categories.Dto;
using UdemyNewMicroservice.Catalog.API.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace UdemyNewMicroservice.Catalog.API.Features.Categories.GetAll
{
    public record GetAllCategoryQuery : IRequest<ServiceResult<List<CategoryDto>>>;

    public class GetAllCategoryQueryHandler(AppDbContext context) : IRequestHandler<GetAllCategoryQuery, ServiceResult<List<CategoryDto>>>
    {
        public async Task<ServiceResult<List<CategoryDto>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var categories = await context.Categories.ToListAsync();
            //var mappedCategories = _mapper.Map<List<CategoryDto>>(categories);
            var categoriesAsDto = categories.Select(x=>new CategoryDto(x.Id, x.Name)).ToList();

            return ServiceResult<List<CategoryDto>>.SucccessAsOk(categoriesAsDto);
        }
    }

    public static class GetAllCategoryEndpoint
    {
        public static RouteGroupBuilder GetAllCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAllCategoryQuery());
                return result.ToGenericResult();
            });

            return group;
        }
    }
}
