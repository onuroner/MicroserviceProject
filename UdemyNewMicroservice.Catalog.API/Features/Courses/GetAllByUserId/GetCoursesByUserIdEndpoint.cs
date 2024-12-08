using AutoMapper;
using MediatR;
using MicroserviceProject.Shared;
using MicroserviceProject.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using UdemyNewMicroservice.Catalog.API.Features.Courses.Dto;
using UdemyNewMicroservice.Catalog.API.Repositories;

namespace UdemyNewMicroservice.Catalog.API.Features.Courses.GetAllByUserId
{
    public record GetCoursesByUserIdQuery(Guid UserId) : IRequest<ServiceResult<List<CourseDto>>>;

    public class GetCoursesByUserIdQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetCoursesByUserIdQuery, ServiceResult<List<CourseDto>>>
    {
        public async Task<ServiceResult<List<CourseDto>>> Handle(GetCoursesByUserIdQuery request, CancellationToken cancellationToken)
        {
            var courses = await context.Courses.Where(x => x.UserId == request.UserId).ToListAsync();
            if (courses is null)
            {
                return ServiceResult<List<CourseDto>>.Error("User has not any courses", System.Net.HttpStatusCode.NotFound);
            }
            
            var categories = await context.Categories.ToListAsync();
            foreach (var course in courses)
            {
                course.Category = categories.Where(x => x.Id == course.CategoryId).First();
            }
            var mappedCourses = mapper.Map<List<CourseDto>>(courses);

            return ServiceResult<List<CourseDto>>.SucccessAsOk(mappedCourses);
        }
    }

    public static class GetCoursesByUserIdEndpoint
    {
        public static RouteGroupBuilder GetCoursesByUserIdGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/user/{userId:guid}", async (Guid UserId, IMediator mediator) =>
            {
                var result = await mediator.Send(new GetCoursesByUserIdQuery(UserId));
                return result.ToGenericResult();
            }).MapToApiVersion(1, 0).WithName("GetCoursesByUserId");

            return group;
        }
    }
}
