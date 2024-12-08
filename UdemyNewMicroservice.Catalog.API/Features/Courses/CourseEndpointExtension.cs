using Asp.Versioning.Builder;
using MediatR;
using MicroserviceProject.Shared.Filters;
using Microsoft.AspNetCore.Mvc;
using UdemyNewMicroservice.Catalog.API.Features.Categories.Create;
using UdemyNewMicroservice.Catalog.API.Features.Categories.GetAll;
using UdemyNewMicroservice.Catalog.API.Features.Categories.GetById;
using UdemyNewMicroservice.Catalog.API.Features.Courses.Create;
using UdemyNewMicroservice.Catalog.API.Features.Courses.Delete;
using UdemyNewMicroservice.Catalog.API.Features.Courses.GetAll;
using UdemyNewMicroservice.Catalog.API.Features.Courses.GetAllByUserId;
using UdemyNewMicroservice.Catalog.API.Features.Courses.GetById;
using UdemyNewMicroservice.Catalog.API.Features.Courses.Update;

namespace UdemyNewMicroservice.Catalog.API.Features.Courses
{
    public static class CourseEndpointExtension
    {
        public static void AddCourseGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/courses").WithTags("Courses")
                .WithApiVersionSet(apiVersionSet)
                .CreateCourseGroupItemEndpoint()
                .GetAllCoursesGroupItemEndpoint()
                .GetCourseByIdGroupItemEndpoint()
                .UpdateCourseGroupItemEndpoint()
                .DeleteCourseGroupItemEndpoint()
                .GetCoursesByUserIdGroupItemEndpoint();
                
        }
    }
}
