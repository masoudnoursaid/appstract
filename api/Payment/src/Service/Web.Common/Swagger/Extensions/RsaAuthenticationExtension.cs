using Application.Common.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Web.Common.Swagger.Extensions;

public static class RsaAuthenticationExtension
{
    public static IServiceCollection ConfigureSwaggerRsaAuthentication(this IServiceCollection services, string jsPath)
    {
        var minifiedString = new Minify(jsPath).GetModifiedData();
        var js = minifiedString;


        services.Configure<SwaggerUIOptions>(opts =>
        {
            opts.InjectJavascript("https://cdnjs.cloudflare.com/ajax/libs/crypto-js/4.1.1/crypto-js.min.js");
            opts.UseRequestInterceptor(js);
        });

        services.Configure<SwaggerGenOptions>(options =>
        {
            options.AddSecurityDefinition("Rsa-PublicKey",
                new OpenApiSecurityScheme
                {
                    Description = "Your application api secret",
                    Name = "Rsa Public Key",
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header
                });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Rsa-PublicKey" }
                    },
                    new string[] { }
                }
            });
        });

        return services;
    }
}