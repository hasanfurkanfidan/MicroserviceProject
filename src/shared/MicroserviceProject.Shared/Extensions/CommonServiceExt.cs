using FluentValidation;
using FluentValidation.AspNetCore;
using MicroserviceProject.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MicroserviceProject.Shared.Extensions
{
    public static class CommonServiceExt
    {
        public static IServiceCollection AddCommonServiceExt(this IServiceCollection services, Type assembly)
        {
            services.AddHttpContextAccessor();
            services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining(assembly));
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining(assembly);
            services.AddAutoMapper(cfg => { }, [assembly.Assembly]);
            services.AddScoped<IIdentityService, IdentityService>();
            return services;
        }
    }
}
