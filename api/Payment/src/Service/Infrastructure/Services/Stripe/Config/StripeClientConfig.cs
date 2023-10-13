using Application.Configuration;
using Application.Services.Payment.Stripe;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services.Stripe.Config;

public class StripeClientConfig : IModuleConfiguration
{
    public IServiceCollection RegisterConfiguration(IServiceCollection services)
    {
        services.AddScoped<IStripeClientService, StripeClientService>();

        return services;
    }
}