
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using UdemyNewMicroservice.Catalog.API.Features.Categories.Create;
using UdemyNewMicroservice.Catalog.API.Options;
using UdemyNewMicroservice.Catalog.API.Repositories;

namespace UdemyNewMicroservice.Catalog.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddOptionsExtensions();
            builder.Services.AddRepositoryExtensions();


            var app = builder.Build();

            app.MapPost("categories", async(CreateCategoryCommand command, IMediator mediator) =>
            {
                var result = mediator.Send(command);
                return new ObjectResult(result)
                {
                    StatusCode = result.Status.GetHashCode()
                };
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
