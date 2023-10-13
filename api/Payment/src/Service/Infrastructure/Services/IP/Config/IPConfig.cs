using Application.Configuration;
using Application.Services.IP;
using Infrastructure.Services.IP.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services.IP.Config;

public class FileConfig : IModuleConfiguration
{
    public IServiceCollection RegisterConfiguration(IServiceCollection services)
    {
        services.AddScoped<IIpService, IpService>();
        return services;
    }
}