using Microsoft.Extensions.Options;

namespace UdemyNewMicroservice.Catalog.API.Options
{
    public static class OptionsExtensions
    {
        public static IServiceCollection AddOptionsExtensions(this IServiceCollection services)
        {
            services.AddOptions<MongoOptions>().BindConfiguration(nameof(MongoOptions)).ValidateDataAnnotations().ValidateOnStart();

            services.AddSingleton<MongoOptions>(sp => sp.GetRequiredService<IOptions<MongoOptions>>().Value);

            return services;
        }
    }
}
