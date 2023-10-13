using Application;
using Application.Common.Validators;
using Appstract.Web.Swagger;
using Domain.Common.BaseTypes;
using Infrastructure.Persistence.Sql;
using Infrastructure.Persistence.Sql.Registration;
using Infrastructure.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Appstract.Web.Extensions;

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

        services.RegisterRepository<string, AppstractDbContext>(typeof(BaseEntity).Assembly.GetName().Name!);

        return services;
    }


    public static IServiceCollection SetupMediateR(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRequestValidator<>), typeof(RequestValidator<>));
        services.AddApplication();
        services.AddSingleton<CustomSentryEventProcessor>();

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