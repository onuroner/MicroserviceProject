using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using System.Reflection.Emit;
using UdemyNewMicroservice.Catalog.API.Features.Categories;

namespace UdemyNewMicroservice.Catalog.API.Repositories
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> modelBuilder)
        {
            modelBuilder.ToCollection("categories");
            modelBuilder.HasKey(x => x.Id);
            modelBuilder.Property(x => x.Id).ValueGeneratedNever();
            modelBuilder.Ignore(x => x.Courses);
        }
    }
}
