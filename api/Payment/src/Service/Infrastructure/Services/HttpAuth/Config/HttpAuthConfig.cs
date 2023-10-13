using Application.Configuration;
using Application.Services.HttpAuth;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services.HttpAuth.Config
{
    public class HttpAuthConfig : IModuleConfiguration
    {
        public IServiceCollection RegisterConfiguration(IServiceCollection services)
        {
            services.AddScoped<IHttpAuthService, HttpAuthService>();
            return services;
        }
    }
}