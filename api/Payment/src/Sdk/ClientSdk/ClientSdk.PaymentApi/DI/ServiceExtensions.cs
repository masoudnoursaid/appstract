using ClientSdk.PaymentApi.V1;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Payment.Common.SDK.Service.Ping;
using Payment.Sdk.Common.Model.Configuration;
using Payment.Sdk.Exceptions;
using Payment.Sdk.Service.Connector;
using Payment.Sdk.Service.PayHubClient;

namespace Payment.Sdk.DI;

public static class DependencyInjection
{
    public static IServiceCollection ConfigurePayHubClient(this IServiceCollection services,
        Action<PaymentConfiguration> config)
    {
        var option = new PaymentConfiguration();
        config(option);

        services.AddSingleton<IPingService, PingService>();

        if (string.IsNullOrEmpty(option.SecurityConfiguration.ApiSecret))
            throw new InvalidApiSecretException(option.SecurityConfiguration.ApiSecret!);

        if (string.IsNullOrEmpty(option.SecurityConfiguration.ApiKey))
            throw new InvalidApiSecretException(option.SecurityConfiguration.ApiKey!);

        services.AddSingleton<PaymentConfiguration>(_ => option);

        services.ConfigureConnectionManager();

        services.ConfigurePaymentMethodApiClient(h => h.BaseAddress = option.ConnectionConfiguration.Uri);
        services.ConfigurePaymentApiClient(h => h.BaseAddress = option.ConnectionConfiguration.Uri);

        services.AddScoped<IPayHubClient, PayHubClient>();

        return services;
    }


    private static IServiceCollection ConfigureConnectionManager(this IServiceCollection services)
    {
        services.AddScoped<IConnectorService, ConnectorService>();

        return services;
    }


    private static IServiceCollection ConfigurePaymentApiClient(this IServiceCollection services,
        Action<HttpClient> config)
    {
        var client = new HttpClient();
        config(client);

        services.AddScoped<IPaymentApiClient, PaymentApiClient>(c =>
        {
            var configuration = c.GetRequiredService<PaymentConfiguration>();
            var logger = c.GetRequiredService<ILogger<PaymentApiClient>>();
            var paymentApiClient = new PaymentApiClient(logger, configuration, client);
            return paymentApiClient;
        });

        return services;
    }

    private static IServiceCollection ConfigurePaymentMethodApiClient(this IServiceCollection services,
        Action<HttpClient> config)
    {
        var client = new HttpClient();
        config(client);

        services.AddScoped<IPaymentMethodApiClient, PaymentMethodApiClient>(c =>
        {
            var configuration = c.GetRequiredService<PaymentConfiguration>();
            var logger = c.GetRequiredService<ILogger<PaymentMethodApiClient>>();
            var paymentMethodApiClient = new PaymentMethodApiClient(logger, configuration, client);
            return paymentMethodApiClient;
        });

        return services;
    }
}