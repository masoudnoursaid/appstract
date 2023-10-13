using System.Diagnostics.CodeAnalysis;
using Appstract.Front.Application.Services;
using Appstract.Front.Application.Services.BackendApis;
using Appstract.Front.Infrastructure;
using Appstract.Front.Infrastructure.Services;
using Appstract.Front.Infrastructure.Services.Authentications;
using Appstract.Front.Infrastructure.Services.BackendApis;
using Appstract.Front.Infrastructure.Services.Configuration;
using Appstract.Front.Infrastructure.Services.DateTimeService;
using Appstract.Front.InfrastructureMock;
using Appstract.Front.InfrastructureMock.FakeServices;
using Appstract.Web.Infrastructure.FakeServices;
using BlazorApplicationInsights;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using UltraTone.ClientSdk.Customer.Web.V1;

namespace Appstract.Front.SharedUI;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppstractServices(
        this IServiceCollection services,
        IAppstractConfiguration configuration)
    {
        services.AddSingleton(configuration);
        services.AddMudServices();
        services.AddBlazoredLocalStorage();
        services.AddAuthorizationCore();
        services.AddBlazorApplicationInsights();
        services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
        services.AddSingleton<AuthenticationState>();
        services.AddSingleton<HttpClientService>();
        services.AddScoped<IErrorMessagesClient, ErrorMessagesClient>();
        services.AddScoped<IProfileClient, ProfileClient>();
        services.AddScoped<ILogger, AiLogger>();
        services.AddScoped<IDateTimeService, DateTimeService>();
        services.AddScoped<IErrorMessageService, ErrorMessageService>();
        services.AddScoped<IPlatformStorageService, PlatformStorageService>();
        services.AddScoped<IErrorMessageService, ErrorMessageService>();
        services.AddScoped<IPaymentMethodService, FakePaymentMethodService>();
        services.AddScoped<IPaymentProcessService, FakePaymentProcessService>();
        services.AddScoped<IVoucherService, FakeVoucherService>();
        services.AddScoped<BaseInfoServices>();


        if (configuration.UseFakeBackend)
        {
            services.AddScoped<IProfileService, FakeProfileService>();
            services.AddFakeInfrastructure();
        }
        else
        {
            services.AddScoped<IProfileService, ProfileService>();
            services.AddInfrastructure();
        }

        services.AddHttpClient(string.Empty, client =>
        {
            client.BaseAddress = configuration.BaseAddress;
        });

        services.AddSingleton(new NativeDeviceDto());

        return services;
    }
}