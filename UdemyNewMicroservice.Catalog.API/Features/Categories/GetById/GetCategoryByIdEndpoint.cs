using AutoMapper;
using MassTransit;
using MediatR;
using MicroserviceProject.Shared;
using MicroserviceProject.Shared.Extensions;
using MicroserviceProject.Shared.Filters;
using Microsoft.EntityFrameworkCore;
using UdemyNewMicroservice.Catalog.API.Features.Categories.Create;
using UdemyNewMicroservice.Catalog.API.Features.Categories.Dto;
using UdemyNewMicroservice.Catalog.API.Features.Categories.GetAll;
using UdemyNewMicroservice.Catalog.API.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace UdemyNewMicroservice.Catalog.API.Features.Categories.GetById
{
    public record GetCategoryByIdQuery(Guid Id) : IRequest<ServiceResult<CategoryDto>>;

    public class GetCategoryByIdQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetCategoryByIdQuery, ServiceResult<CategoryDto>>
    {
        public async Task<ServiceResult<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await context.Categories.FindAsync(request.Id, cancellationToken);
            if(category is null)
            {
                return ServiceResult<CategoryDto>.Error("Category does not exists", System.Net.HttpStatusCode.NotFound);
            }
            var mappedCategory = mapper.Map<CategoryDto>(category);
         
            return ServiceResult<CategoryDto>.SucccessAsOk(mappedCategory);
        }
    }

    public static class GetCategoryByIdEndpoint
    {
        public static RouteGroupBuilder GetCategoryByIdGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var result = await mediator.Send(new GetCategoryByIdQuery(id));
                return result.ToGenericResult();
            }).MapToApiVersion(1, 0).WithName("GetCategoryById");

            return group;
        }
    }
}
