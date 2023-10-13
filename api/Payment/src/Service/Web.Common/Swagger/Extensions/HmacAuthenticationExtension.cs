using Application.Common.Consts.Authentication;
using Application.Common.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Web.Common.Swagger.Extensions;

public static class HmacAuthenticationExtension
{
    public static IServiceCollection ConfigureSwaggerHmacAuthentication(this IServiceCollection services, string jsPath)
    {
        const string ApiSecretHeaderName = "Api-Secret";

        var minifiedString = new Minify(jsPath).GetModifiedData();
        var js = minifiedString
            .Replace("{HmacAuthentication.DATE_HEADER}", HmacAuthentication.DATE_HEADER)
            .Replace("{ApiSecretHeaderName}", ApiSecretHeaderName)
            .Replace("{HmacAuthentication.SIGNATURE_HEADER}", HmacAuthentication.SIGNATURE_HEADER)
            .Replace("{HmacAuthentication.NONCE_HEADER}", HmacAuthentication.NONCE_HEADER);


        services.Configure<SwaggerUIOptions>(opts =>
        {
            opts.InjectJavascript("https://cdnjs.cloudflare.com/ajax/libs/crypto-js/4.1.1/crypto-js.min.js");
            opts.UseRequestInterceptor(js);
        });

        services.Configure<SwaggerGenOptions>(options =>
        {
            options.AddSecurityDefinition("ApiSecret",
                new OpenApiSecurityScheme
                {
                    Description = "Your application api secret",
                    Name = ApiSecretHeaderName,
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header
                });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "ApiSecret" }
                    },
                    new string[] { }
                }
            });
        });

        return services;
    }
}