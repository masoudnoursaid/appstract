using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Appstract.Web.Swagger;

public class SwaggerBasicAuthMiddleware
{
    private readonly IConfiguration _configuration;
    private readonly RequestDelegate _next;

    public SwaggerBasicAuthMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var expectedUsername = _configuration.GetValue<string>("Swagger:Username");
        var expectedPassword = _configuration.GetValue<string>("Swagger:Password");

        if (string.IsNullOrWhiteSpace(expectedUsername) && string.IsNullOrWhiteSpace(expectedPassword))
        {
            await _next.Invoke(context).ConfigureAwait(false);
            return;
        }

        string? authHeader = context.Request.Headers["Authorization"];

        if (authHeader != null && authHeader.StartsWith("Basic "))
        {
            var header = AuthenticationHeaderValue.Parse(authHeader);
            var inBytes = Convert.FromBase64String(header.Parameter!);
            var credentials = Encoding.UTF8.GetString(inBytes).Split(':');
            var username = credentials[0];
            var password = credentials[1];

            if (username.Equals(expectedUsername) && password.Equals(expectedPassword))
            {
                await _next.Invoke(context).ConfigureAwait(false);
                return;
            }
        }

        context.Response.Headers["WWW-Authenticate"] = "Basic";
        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
    }
}