using AutoMapper;
using MassTransit;
using MediatR;
using MicroserviceProject.Shared;
using Microsoft.EntityFrameworkCore;
using UdemyNewMicroservice.Catalog.API.Repositories;

namespace UdemyNewMicroservice.Catalog.API.Features.Courses.Create
{
    public class CreateCourseCommandHandler(AppDbContext context, IMapper mapper) : IRequestHandler<CreateCourseCommand, ServiceResult<Guid>>
    {
        public async Task<ServiceResult<Guid>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {

            var hasCategory = await context.Categories.AnyAsync(x => x.Id == request.CategoryId);

            if (!hasCategory)
            {
                return ServiceResult<Guid>.Error("Category not found.", $"Category with id({request.CategoryId}) not found.", System.Net.HttpStatusCode.NotFound);
            }

            var hasCourse = await context.Courses.AnyAsync(x => x.Name == request.Name);

            if (hasCourse)
            {
                return ServiceResult<Guid>.Error("Course already exists.", $"Course with name({request.Name}) already exists.", System.Net.HttpStatusCode.NotFound);
            }

            var newCourse = mapper.Map<Course>(request);
            newCourse.CreatedDate = DateTime.Now;
            newCourse.Id = NewId.NextSequentialGuid();
            newCourse.Feature = new Feature()
            {
                EducatorFullName = "Onur Öner",
                Duration = 10,
                Rating = 0
            };

            context.Courses.Add(newCourse);
            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult<Guid>.SucccessAsCreated(newCourse.Id, $"api/courses/{newCourse.Id}");
        }
    }
}
