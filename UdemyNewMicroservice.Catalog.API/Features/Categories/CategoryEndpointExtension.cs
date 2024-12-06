using MediatR;
using Microsoft.AspNetCore.Mvc;
using UdemyNewMicroservice.Catalog.API.Features.Categories.Create;

namespace UdemyNewMicroservice.Catalog.API.Features.Categories
{
    public static class CategoryEndpointExtension
    {
        public static void AddCategoryGroupEndpointExt(this WebApplication app)
        {
            app.MapGroup("api/categories").CreateCategoryGroupItemEndpoint();
        }
    }
}
