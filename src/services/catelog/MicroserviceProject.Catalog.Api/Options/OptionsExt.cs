using System.Runtime.CompilerServices;

namespace MicroserviceProject.Catalog.Api.Options
{
    public static class OptionsExt
    {
        public static IServiceCollection AddOptionsExt(this IServiceCollection services)
        {
            services.AddOptions<MongoOptions>().BindConfiguration(nameof(MongoOptions)).ValidateDataAnnotations().ValidateOnStart();

            return services;
        }
    }
}
