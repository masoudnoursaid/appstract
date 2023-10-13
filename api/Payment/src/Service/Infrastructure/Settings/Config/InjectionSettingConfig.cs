using Application.Configuration;
using Application.Settings.Environments.BillPlz;
using Application.Settings.Environments.GlobalPaymentApi;
using Application.Settings.Environments.Mollie;
using Application.Settings.Environments.Paypal;
using Application.Settings.Environments.Stripe;
using Infrastructure.Settings.BillPlz;
using Infrastructure.Settings.GlobalPaymentApi;
using Infrastructure.Settings.Mollie;
using Infrastructure.Settings.Paypal;
using Infrastructure.Settings.Stripe;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Settings.Config;

public class InjectionSettingConfig : IModuleConfiguration
{
    public IServiceCollection RegisterConfiguration(IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

        services.AddSingleton<IPaypalEnvironmentSetting>(cnf =>
        {
            var setting = configuration.Get<PaypalEnvironmentSetting>();
            return setting!;
        });

        services.AddSingleton<IGlobalPaymentApiEnvironmentSetting>(cnf =>
        {
            var setting = configuration.Get<GlobalPaymentApiEnvironmentSetting>();
            return setting!;
        });

        services.AddSingleton<IBillPlzEnvironmentSetting>(cnf =>
        {
            var setting = configuration.Get<BillPlzEnvironmentSetting>();
            return setting!;
        });

        services.AddSingleton<IStripeEnvironmentSetting>(cnf =>
        {
            var setting = configuration.Get<StripeEnvironmentSetting>();
            return setting!;
        });
        
        services.AddSingleton<IMollieEnvironmentSetting>(cnf =>
        {
            var setting = configuration.Get<MollieEnvironmentSetting>();
            return setting!;
        });

        return services;
    }
}