
using MediatR;
using MicroserviceProject.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using UdemyNewMicroservice.Catalog.API.Features.Categories;
using UdemyNewMicroservice.Catalog.API.Features.Categories.Create;
using UdemyNewMicroservice.Catalog.API.Features.Courses;
using UdemyNewMicroservice.Catalog.API.Options;
using UdemyNewMicroservice.Catalog.API.Repositories;

namespace UdemyNewMicroservice.Catalog.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddOptionsExtensions();
            builder.Services.AddRepositoryExtensions();
            builder.Services.AddCommonServiceExt(typeof(Program));
            builder.Services.AddVersioningExt();

            var app = builder.Build();
            app.AddCategoryGroupEndpointExt(app.AddVersionSetExt());
            app.AddCourseGroupEndpointExt(app.AddVersionSetExt());
            app.AddSeedDataExt().ContinueWith(x =>
            {
                if(x.IsFaulted)
                {
                    Console.WriteLine(x.Exception.Message);
                }
                else
                {
                    Console.WriteLine("Seed data created successfully.");
                }
            });
            


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.Run();
        }
    }
}
