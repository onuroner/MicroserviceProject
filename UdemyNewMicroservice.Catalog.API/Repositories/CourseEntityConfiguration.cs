using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using System.Reflection.Emit;
using UdemyNewMicroservice.Catalog.API.Features.Courses;

namespace UdemyNewMicroservice.Catalog.API.Repositories
{
    public class CourseEntityConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> modelBuilder)
        {
            modelBuilder.ToCollection("courses");
            modelBuilder.HasKey(x => x.Id);
            modelBuilder.Property(x => x.Id).ValueGeneratedNever();
            modelBuilder.Property(x => x.Name).HasElementName("name").HasMaxLength(100);
            modelBuilder.Property(x => x.Description).HasElementName("description").HasMaxLength(1000);
            modelBuilder.Property(x => x.CreatedDate).HasElementName("created");
            modelBuilder.Property(x => x.UserId).HasElementName("userId");
            modelBuilder.Property(x => x.CategoryId).HasElementName("categoryId");
            modelBuilder.Property(x => x.ImageUrl).HasElementName("imageUrl");
            modelBuilder.Ignore(x => x.Category);

            modelBuilder.OwnsOne(f => f.Feature, feature =>
            {
                feature.HasElementName("feature");
                feature.Property(x => x.Duration).HasElementName("duration");
                feature.Property(x => x.Rating).HasElementName("rating");
                feature.Property(x => x.EducatorFullName).HasElementName("educatorFullName").HasMaxLength(100);
            });
        }
    }
}
