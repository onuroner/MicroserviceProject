using MassTransit;
using MongoDB.Driver;
using UdemyNewMicroservice.Catalog.API.Features.Categories;
using UdemyNewMicroservice.Catalog.API.Features.Courses;

namespace UdemyNewMicroservice.Catalog.API.Repositories
{
    public static class SeedData
    {
        public static async Task AddSeedDataExt(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.AutoTransactionBehavior = Microsoft.EntityFrameworkCore.AutoTransactionBehavior.Never;

                if (!dbContext.Categories.Any())
                {
                    var categories = new List<Category>()
                    {
                        new() {Id = NewId.NextSequentialGuid(), Name = "Development"},
                        new() {Id = NewId.NextSequentialGuid(), Name = "Business"},
                        new() {Id = NewId.NextSequentialGuid(), Name = "IT & Software"},
                        new() {Id = NewId.NextSequentialGuid(), Name = "Office Productivity"},
                        new() {Id = NewId.NextSequentialGuid(), Name = "Personal Development"}
                    };

                    dbContext.Categories.AddRange(categories);
                    dbContext.SaveChanges();
                }

                if (!dbContext.Courses.Any())
                {
                    var category = dbContext.Categories.First();
                    var randomUserId = NewId.NextGuid();
                    var courses = new List<Course>()
                    {
                        new() {Id = NewId.NextSequentialGuid(), Name = "C#", Description="C# Course", Price=100, UserId=randomUserId, CategoryId = category.Id, Feature = new Feature(){ Duration=10, Rating=0, EducatorFullName="Onur Öner" }, CreatedDate = DateTime.Now, ImageUrl = "image url"},

                        new() {Id = NewId.NextSequentialGuid(), Name = "Flutter", Description="Flutter Course", Price=140, UserId=randomUserId, CategoryId = category.Id, Feature = new Feature(){ Duration=16, Rating=0, EducatorFullName="Onur Öner" }, CreatedDate = DateTime.Now, ImageUrl = "image url"},

                       new() {Id = NewId.NextSequentialGuid(), Name = "Node.js", Description="Node.js Course", Price=129, UserId=randomUserId, CategoryId = category.Id, Feature = new Feature(){ Duration=12, Rating=0, EducatorFullName="Onur Öner" }, CreatedDate = DateTime.Now, ImageUrl = "image url"},
                    };

                    dbContext.Courses.AddRange(courses);
                    dbContext.SaveChanges();
                }
                
            }
        }
    }
}
