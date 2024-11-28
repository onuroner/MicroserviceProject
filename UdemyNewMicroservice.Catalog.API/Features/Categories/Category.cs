using UdemyNewMicroservice.Catalog.API.Features.Courses;
using UdemyNewMicroservice.Catalog.API.Repositories;

namespace UdemyNewMicroservice.Catalog.API.Features.Categories
{
    public class Category:BaseEntity
    {
        public string Name { get; set; } = default!;
        public List<Course>? Courses { get; set; }
    }
}
