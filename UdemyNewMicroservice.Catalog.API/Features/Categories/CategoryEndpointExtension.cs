﻿using MediatR;
using MicroserviceProject.Shared.Filters;
using Microsoft.AspNetCore.Mvc;
using UdemyNewMicroservice.Catalog.API.Features.Categories.Create;
using UdemyNewMicroservice.Catalog.API.Features.Categories.GetAll;
using UdemyNewMicroservice.Catalog.API.Features.Categories.GetById;

namespace UdemyNewMicroservice.Catalog.API.Features.Categories
{
    public static class CourseEndpointExtension
    {
        public static void AddCategoryGroupEndpointExt(this WebApplication app)
        {
            app.MapGroup("api/categories").WithTags("Categories")
                .CreateCategoryGroupItemEndpoint()
                .GetAllCategoryGroupItemEndpoint()
                .GetCategoryByIdGroupItemEndpoint();
        }
    }
}
