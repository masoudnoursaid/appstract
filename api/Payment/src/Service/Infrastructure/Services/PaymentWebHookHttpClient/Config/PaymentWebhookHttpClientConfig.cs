using Application.Configuration;
using Infrastructure.Common.PaymentHttpClient;
using Infrastructure.Common.PaymentHttpClient.Handler;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.PaymentWebHookHttpClient.Config;

public class PaymentWebhookHttpClientConfig : IModuleConfiguration
{
    public IServiceCollection RegisterConfiguration(IServiceCollection services)
    {
        services.AddScoped<IPaymentWebHookHttpClient>(p =>
        {
            var baseLogger = p.GetRequiredService<ILogger<HmacHttpClient>>();
            var targetLogger = p.GetRequiredService<ILogger<PaymentWebHookHttpClient>>();
            var messageHandler = new RetryHandler(new HttpClientHandler(), maxRetries: 5);
            var client = new PaymentWebHookHttpClient(targetLogger, baseLogger, messageHandler);

            return client;
        });

        return services;
    }
}