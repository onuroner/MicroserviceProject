﻿using MediatR;
using MicroserviceProject.Shared;

namespace UdemyNewMicroservice.Catalog.API.Features.Courses.Create
{
    public record CreateCourseCommand(string Name, string Description, decimal Price,  string? ImageUrl, Guid CategoryId ) : IRequest<ServiceResult<Guid>>
    {
    }
}
