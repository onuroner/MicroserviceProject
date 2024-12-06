using AutoMapper;
using MediatR;
using MicroserviceProject.Shared;
using MicroserviceProject.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using UdemyNewMicroservice.Catalog.API.Features.Categories;
using UdemyNewMicroservice.Catalog.API.Features.Categories.Dto;
using UdemyNewMicroservice.Catalog.API.Features.Courses.Dto;
using UdemyNewMicroservice.Catalog.API.Repositories;

namespace UdemyNewMicroservice.Catalog.API.Features.Courses.GetAll
{
    public record GetAllCoursesQuery : IRequest<ServiceResult<List<CourseDto>>>;

    public class GetAllCoursesQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetAllCoursesQuery, ServiceResult<List<CourseDto>>>
    {
        public async Task<ServiceResult<List<CourseDto>>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            var courses = await context.Courses.ToListAsync(cancellationToken);
            var categories = await context.Categories.ToListAsync(cancellationToken);

            foreach (var course in courses)
            {
                course.Category = categories.Where(x => x.Id == course.CategoryId).First();
            }

            var mappedCourses = mapper.Map<List<CourseDto>>(courses);


            return ServiceResult<List<CourseDto>>.SucccessAsOk(mappedCourses);
        }
    }

    public static class GetAllCoursesEndpoint
    {
        public static RouteGroupBuilder GetAllCoursesGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAllCoursesQuery());
                return result.ToGenericResult();
            }).WithName("GetAllCourses");

            return group;
        }
    }
}
