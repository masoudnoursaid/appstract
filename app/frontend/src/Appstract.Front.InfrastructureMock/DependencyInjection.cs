using Appstract.Front.InfrastructureMock.Registrars;
using Microsoft.Extensions.DependencyInjection;

namespace Appstract.Front.InfrastructureMock;

public static class DependencyInjection
{
    public static IServiceCollection AddFakeInfrastructure(this IServiceCollection services)
    {
        services.AddServices();

        return services;
    }
}