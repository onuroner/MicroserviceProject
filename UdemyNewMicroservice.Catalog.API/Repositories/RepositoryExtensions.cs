using Microsoft.Extensions.Options;
using MongoDB.Driver;
using UdemyNewMicroservice.Catalog.API.Options;

namespace UdemyNewMicroservice.Catalog.API.Repositories
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositoryExtensions(this IServiceCollection services)
        {
           services.AddSingleton<IMongoClient, MongoClient>(sp =>
            {
                var options = sp.GetRequiredService<MongoOptions>();
                return new MongoClient(options.ConnectionString);
            });
            services.AddScoped(sp =>
            {
                var mongoClient = sp.GetRequiredService<IMongoClient>();
                var options = sp.GetRequiredService<MongoOptions>();
                return AppDbContext.Create(mongoClient.GetDatabase(options.DatabaseName));
            });

            return services;
        }
    }
}
