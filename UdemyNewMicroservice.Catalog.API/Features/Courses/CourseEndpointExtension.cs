using MediatR;
using MicroserviceProject.Shared.Filters;
using Microsoft.AspNetCore.Mvc;
using UdemyNewMicroservice.Catalog.API.Features.Categories.Create;
using UdemyNewMicroservice.Catalog.API.Features.Categories.GetAll;
using UdemyNewMicroservice.Catalog.API.Features.Categories.GetById;
using UdemyNewMicroservice.Catalog.API.Features.Courses.Create;
using UdemyNewMicroservice.Catalog.API.Features.Courses.GetAll;

namespace UdemyNewMicroservice.Catalog.API.Features.Courses
{
    public static class CourseEndpointExtension
    {
        public static void AddCourseGroupEndpointExt(this WebApplication app)
        {
            app.MapGroup("api/courses").WithTags("Courses")
                .CreateCourseGroupItemEndpoint()
                .GetAllCoursesGroupItemEndpoint();
                
        }
    }
}
