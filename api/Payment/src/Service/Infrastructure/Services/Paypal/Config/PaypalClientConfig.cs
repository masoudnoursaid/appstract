using Application.Configuration;
using Application.Services.Payment.Paypal;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services.Paypal.Config;

public class PaypalClientConfig : IModuleConfiguration
{
    public IServiceCollection RegisterConfiguration(IServiceCollection services)
    {
        services.AddScoped<IPaypalClientService, PaypalClientService>();

        return services;
    }
}