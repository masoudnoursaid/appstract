using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Web.Common.Middlewares;

public class ErrorLoggingMiddleware
{
    private readonly ILogger _logger;
    private readonly RequestDelegate _next;

    public ErrorLoggingMiddleware(RequestDelegate next, ILogger<ErrorLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        await _next(httpContext);

        switch (httpContext.Response.StatusCode)
        {
            case 401:
            case 403:
                var ipAddress = "NAN";
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