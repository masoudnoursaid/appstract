using Application.Configuration;
using Application.Services.Payment.BillPlz;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services.BillPlz.Config;

public class BillPlzClientConfig : IModuleConfiguration
{
    public IServiceCollection RegisterConfiguration(IServiceCollection services)
    {
        services.AddScoped<IBillPlzClientService, BillPlzClientService>();

        return services;
    }
}