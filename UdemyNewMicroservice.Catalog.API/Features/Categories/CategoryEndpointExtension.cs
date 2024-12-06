using MediatR;
using MicroserviceProject.Shared.Filters;
using Microsoft.AspNetCore.Mvc;
using UdemyNewMicroservice.Catalog.API.Features.Categories.Create;
using UdemyNewMicroservice.Catalog.API.Features.Categories.GetAll;

namespace UdemyNewMicroservice.Catalog.API.Features.Categories
{
    public static class CategoryEndpointExtension
    {
        public static void AddCategoryGroupEndpointExt(this WebApplication app)
        {
            app.MapGroup("api/categories")
                .CreateCategoryGroupItemEndpoint()
                .GetAllCategoryGroupItemEndpoint();
        }
    }
}
