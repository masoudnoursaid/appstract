using Application.Common.Validators;
using Domain.Common.BaseTypes;
using Infrastructure.DependencyInjection;
using Infrastructure.Persistence.Sql;
using Infrastructure.Persistence.Sql.Registrator;
using Infrastructure.Validator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Web.Common.Swagger;
using DependencyInjection = Application.DependencyInjection;

namespace Web.Common.Extensions;

using RegisterSQLRepository = RepositoryRegistrator;

public static class StartUpExtensions
{
    public static IServiceCollection SetupCustomSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen();
        services.AddCustomSwagger();
        services.TryAddEnumerable(ServiceDescriptor
            .Transient<IApiDescriptionProvider, ClientBasedApiDescriptionProvider>());
        return services;
    }

    public static IServiceCollection SetupControllers(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
            options.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
        });

        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        services.AddHttpContextAccessor();

        return services;
    }

    public static IServiceCollection SetupRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext(configuration);

        services.RegisterRepository<string, PaymentDbContext>(typeof(BaseEntity).Assembly.GetName().Name!);

        return services;
    }


    public static IServiceCollection SetupMediateR(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRequestValidator<>), typeof(RequestValidator<>));
        services.ModuleRegistry(typeof(DependencyInjection).Assembly,
            typeof(Infrastructure.DependencyInjection.DependencyInjection).Assembly);
        services.AddSingleton<CustomSentryEventProcessor>();

        return services;
    }


    public static IServiceCollection DbInitByEnv(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.Get<DbSettings>()?.Migrate ?? false)
        {
            var logger = services.BuildServiceProvider().GetRequiredService<ILogger<PaymentDbContext>>();

            try
            {
                var db = services.BuildServiceProvider().GetRequiredService<PaymentDbContext>();
                db.Database.EnsureCreated();
                db.Database.Migrate();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }
        }

        return services;
    }


    public static IApplicationBuilder SetupSwaggerUI(this IApplicationBuilder app)
    {
        var apiDescriptionGroupCollectionProvider =
            app.ApplicationServices.GetRequiredService<IApiDescriptionGroupCollectionProvider>();
        app.UseCustomSwaggerUI(apiDescriptionGroupCollectionProvider);

        return app;
    }
}