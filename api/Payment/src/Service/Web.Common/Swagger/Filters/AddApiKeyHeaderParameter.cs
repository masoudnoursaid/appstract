using Application.Common.Consts.Authentication;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Parameter = Microsoft.OpenApi.Models.OpenApiParameter;


namespace Web.Common.Swagger.Filters;

public class AddApiKeyHeaderParameter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<Parameter>();

        operation.Parameters.Add(new Parameter
        {
            In = ParameterLocation.Header,
            Name = HmacAuthentication.API_KEY_HEADER,
            Style = ParameterStyle.Simple,
            Description = "Your application api key"
        });
    }
}