using Microsoft.Extensions.DependencyInjection;

namespace Application.Configuration;

public interface IModuleConfiguration
{
    IServiceCollection RegisterConfiguration(IServiceCollection services);
}