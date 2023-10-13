using System.Reflection;
using Application.Common.PipelineBehaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.AddOpenBehavior(typeof(CommonErrorsPipelineBehavior<,>));
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });
        services.AddValidatorsFromAssemblies(new List<Assembly> { Assembly.GetExecutingAssembly() });

        return services;
    }
}