using Application.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence.Sql.Seeds.Config;

public class SeedServiceConfiguration : IModuleConfiguration
{
    public IServiceCollection RegisterConfiguration(IServiceCollection services)
    {
        services.AddScoped<ISeedService, SeedService>();

        return services;
    }
}