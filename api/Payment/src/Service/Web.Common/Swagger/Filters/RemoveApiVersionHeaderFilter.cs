using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Web.Common.Swagger.Filters;

public class RemoveApiVersionHeaderFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var versionParameter = operation.Parameters.FirstOrDefault(p => p.Name == "x-api-version");

        if (versionParameter != null &&
            context.ApiDescription.Properties.TryGetValue(typeof(ApiVersion), out var apiVersion))
        {
            var version = apiVersion.ToString()!;
            if (version == "1.0")
                operation.Parameters.Remove(versionParameter);
            else
                versionParameter.Example = new OpenApiString(version);
        }
    }
}