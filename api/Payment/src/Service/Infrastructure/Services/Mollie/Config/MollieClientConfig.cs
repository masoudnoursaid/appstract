using Application.Configuration;
using Application.Services.Payment.Mollie;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services.Mollie.Config;

public class MollieClientConfig : IModuleConfiguration
{
    public IServiceCollection RegisterConfiguration(IServiceCollection services)
    {
        services.AddScoped<IMollieClientService, MollieClientService>();

        return services;
    }
}