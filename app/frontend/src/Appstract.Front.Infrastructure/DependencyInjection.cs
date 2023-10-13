using Appstract.Front.Infrastructure.Registrars;
using Microsoft.Extensions.DependencyInjection;

namespace Appstract.Front.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services
            .AddBackendApisServices()
            .AddServices();

        return services;
    }
}