using Microsoft.Extensions.Configuration;

namespace MicroserviceProject.Shared.Extensions
{
    public static class OptionsPatternExt
    {
        public static T GetConfiguration<T>(this IConfiguration configuration) where T : class
        {
            return configuration.GetSection(typeof(T).Name).Get<T>() ?? null;
        }
    }
}
