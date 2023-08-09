using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;
using Web.Common.Swagger.Filters;

namespace Web.Common.Swagger;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiDescriptionGroupCollectionProvider _apiDescriptionGroupCollectionProvider;
    private readonly IConfiguration _configuration;

    public ConfigureSwaggerOptions(
        IConfiguration configuration,
        IApiDescriptionGroupCollectionProvider apiDescriptionGroupCollectionProvider)
    {
        _configuration = configuration;
        _apiDescriptionGroupCollectionProvider = apiDescriptionGroupCollectionProvider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        // add swagger document for every API group discovered
        foreach (ApiDescriptionGroup apiDescriptionGroup in _apiDescriptionGroupCollectionProvider.ApiDescriptionGroups.Items)
        {
            options.SwaggerDoc(apiDescriptionGroup.GroupName, CreateVersionInfo(apiDescriptionGroup.Items.First()));
        }
        
        string server = _configuration.GetValue<string>("Server")!;
        options.AddServer(new OpenApiServer { Url = server });

        options.EnableAnnotations();

        options.AddEnumsWithValuesFixFilters();
        options.UseAllOfToExtendReferenceSchemas();

        options.SchemaFilter<FrontendClientTypeSchemaFilter>();
        options.SchemaFilter<PurchaseStatusTypeSchemaFilter>();

        options.OperationFilter<ErrorCodeOperationFilter>();
        options.OperationFilter<RemoveApiVersionHeaderFilter>();

        options.MapType<decimal>(() => new OpenApiSchema { Type = "number", Format = "decimal" });
    }

    private OpenApiInfo CreateVersionInfo(ApiDescription description)
    {
        OpenApiInfo info = new()
        {
            Title = $"UltraTone.Backend.Legacy.Customer-{description.GroupName}",
            Version = description.GetApiVersion().ToString()
        };

        if (description.IsDeprecated())
        {
            info.Description += " This API version has been deprecated.";
        }

        return info;
    }
}