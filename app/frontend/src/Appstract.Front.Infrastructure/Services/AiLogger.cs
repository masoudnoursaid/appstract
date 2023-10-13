using BlazorApplicationInsights;
using Microsoft.Extensions.Logging;

namespace Appstract.Front.Infrastructure.Services;

public class AiLogger : ILogger
{
    private readonly IApplicationInsights _applicationInsights;
    
    public AiLogger(IApplicationInsights applicationInsights)
    {
        _applicationInsights = applicationInsights;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        string message = formatter(state, exception) + " - " + exception?.Message;
        switch (logLevel)
        {
            case LogLevel.None:
            case LogLevel.Trace:
            case LogLevel.Debug:
            case LogLevel.Information:
            case LogLevel.Warning:
                _applicationInsights.TrackEvent(message);
                break;
            case LogLevel.Error:
            case LogLevel.Critical:
                Error error = new()
                {
                    Message = message,
                    Name = exception?.GetType().Name ?? string.Empty,
                    Stack = exception?.StackTrace ?? string.Empty
                };
                _applicationInsights.TrackException(error);
                break;
            default:
                _applicationInsights.TrackEvent(message);
                break;
        }
    }

    public bool IsEnabled(LogLevel logLevel) => true;

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => default;
}