using System.Reflection;
using Application.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection;

public static class ModuleRegistryExtension
{
    public static IServiceCollection ModuleRegistry(this IServiceCollection service, params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes()
                .Where(t => typeof(IModuleConfiguration).IsAssignableFrom(t) &&
                            t.Name.ToLower() != nameof(IModuleConfiguration).ToLower()).ToList();
            foreach (var type in types)
            {
                var obj = (IModuleConfiguration)(Activator.CreateInstance(type) ?? throw new Exception());
                obj.RegisterConfiguration(service);
            }
        }

        return service;
    }
}