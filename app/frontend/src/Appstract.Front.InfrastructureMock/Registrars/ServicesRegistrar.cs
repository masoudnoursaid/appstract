using Appstract.Front.Application.Services.BackendApis;
using Appstract.Front.InfrastructureMock.FakeServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Appstract.Front.InfrastructureMock.Registrars;

public static class ServicesRegistrar
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, FakeAccountService>();

        return services;
    }
}