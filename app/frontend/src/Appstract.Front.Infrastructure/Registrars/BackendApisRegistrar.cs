using ClientSdk.Customer.Web.V1;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Appstract.Front.Infrastructure.Registrars;

public static class BackendApisRegistrar
{
    public static IServiceCollection AddBackendApisServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountClient, AccountClient>();

        return services;
    }
}