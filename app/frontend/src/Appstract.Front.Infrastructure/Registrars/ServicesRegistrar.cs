using Appstract.Front.Application.Services.BackendApis;
using Appstract.Front.Infrastructure.Services.BackendApis;
using Microsoft.Extensions.DependencyInjection;

namespace Appstract.Front.Infrastructure.Registrars;

public static class ServicesRegistrar
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();

        return services;
    }
}