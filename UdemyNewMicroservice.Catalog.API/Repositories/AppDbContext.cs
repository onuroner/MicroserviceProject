﻿using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;
using System.Reflection;
using UdemyNewMicroservice.Catalog.API.Features.Categories;
using UdemyNewMicroservice.Catalog.API.Features.Courses;

namespace UdemyNewMicroservice.Catalog.API.Repositories
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }

        public static AppDbContext Create(IMongoDatabase database)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>().UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName);
            return  new AppDbContext(optionsBuilder.Options);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
