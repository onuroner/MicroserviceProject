using MassTransit;
using MediatR;
using MicroserviceProject.Shared;
using Microsoft.EntityFrameworkCore;
using UdemyNewMicroservice.Catalog.API.Repositories;

namespace UdemyNewMicroservice.Catalog.API.Features.Categories.Create
{
    public class CreateCategoryCommandHandler(AppDbContext context) : IRequestHandler<CreateCategoryCommand, ServiceResult<CreateCategoryResponse>>
    {
        public async Task<ServiceResult<CreateCategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var existCategory = await context.Categories.AnyAsync(x => x.Name == request.Name, cancellationToken: cancellationToken);

            if (existCategory)
            {
                return ServiceResult<CreateCategoryResponse>.Error("Category name already exists", $"The category name {request.Name} already exist.", System.Net.HttpStatusCode.BadRequest);
            }

            var category = new Category { Name = request.Name, Id = NewId.NextSequentialGuid() };
        
            await context.AddAsync(category, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return ServiceResult<CreateCategoryResponse>.SucccessAsCreated(new CreateCategoryResponse(category.Id), string.Empty);
        }
    }
}
