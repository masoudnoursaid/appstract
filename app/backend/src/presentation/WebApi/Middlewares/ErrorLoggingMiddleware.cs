using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Appstract.WebApi.Middlewares;

public class ErrorLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ErrorLoggingMiddleware(RequestDelegate next, ILogger<ErrorLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext, [FromServices] IIpService ipService)
    {
        await _next(httpContext);

        switch (httpContext.Response.StatusCode)
        {
            case 401:
            case 403:
                string? ipAddress = ipService.GetRawRemoteIpAddress();
                string? authorizationHeader = httpContext.Request.Headers["Authorization"];

                _logger.LogWarning(
                    "HTTP status code '{StatusCode}' returned for authorization header '{AuthorizationHeader}' sent from '{RemoteIpAddress}'",
                    httpContext.Response.StatusCode,
                    authorizationHeader,
                    ipAddress);
                break;
        }
    }
}