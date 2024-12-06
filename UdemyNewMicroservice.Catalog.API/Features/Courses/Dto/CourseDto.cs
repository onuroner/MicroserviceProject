using UdemyNewMicroservice.Catalog.API.Features.Categories.Dto;

namespace UdemyNewMicroservice.Catalog.API.Features.Courses.Dto
{
    public record CourseDto(Guid Id, string Name, string Description, decimal Price, string ImageUrl, CategoryDto Category, FeatureDto Feature);
    
}
