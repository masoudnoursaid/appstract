using Application;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Web.Common.Swagger.Filters;

public class PurchaseStatusTypeSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema.Title == SchemaTitle.PURCHASE_STATUS)
        {
            schema.Enum = new List<IOpenApiAny>
            {
                new OpenApiString("Successful"), new OpenApiString("Failed"), new OpenApiString("Cancelled"),
            };
        }
    }
}