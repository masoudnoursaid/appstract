using Appstract.Web.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;

namespace Appstract.Web.Extensions;

public static class SwaggerExtensions
{
    private const string SWAGGER_PREFIX_PATH = "_sw";

    public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen();
        services.ConfigureOptions<ConfigureSwaggerOptions>();
        return services;
    }

    public static IApplicationBuilder UseCustomSwaggerUI(
        this IApplicationBuilder app,
        IApiDescriptionGroupCollectionProvider apiDescriptionGroupCollectionProvider)
    {
        app.UseWhen(context => context.Request.Path.StartsWithSegments($"/{SWAGGER_PREFIX_PATH}"), appBuilder =>
        {
            appBuilder.UseMiddleware<SwaggerBasicAuthMiddleware>();
        });

        app.UseSwagger(options =>
        {
            options.RouteTemplate = $"{SWAGGER_PREFIX_PATH}/{{documentName}}/swagger.json";
        });
        app.UseSwaggerUI(options =>
        {
            options.RoutePrefix = SWAGGER_PREFIX_PATH;

            foreach (string groupName in apiDescriptionGroupCollectionProvider.ApiDescriptionGroups.Items.Select(a => a.GroupName!))
            {
                options.SwaggerEndpoint($"/{SWAGGER_PREFIX_PATH}/{groupName}/swagger.json", groupName);
            }

            options.EnableDeepLinking();
        });
        return app;
    }
}