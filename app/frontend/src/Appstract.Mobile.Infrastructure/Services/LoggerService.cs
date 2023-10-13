using Microsoft.Extensions.Logging;
using Sentry;
using ILogger = Appstract.Mobile.Application.Services.ILogger;

namespace Appstract.Front.Mobile.Infrastructure.Services;

public class LoggerService : Appstract.Mobile.Application.Services.ILogger
{
    public void LogException(Exception e)
    {
        SentrySdk.CaptureException(e);
    }
    
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
        Func<TState, Exception, string> formatter)
    {
        if (exception != null)
        {
            SentrySdk.CaptureException(exception);
        }
        else
        {
            string message = formatter(state, null);
            SentryLevel sentryLevel = _sentryLevel(logLevel);
            SentrySdk.CaptureMessage(message
                , sentryLevel);
        }
    }

    private SentryLevel _sentryLevel(LogLevel logLevel)
    {
        return logLevel switch
        {
            LogLevel.Debug => SentryLevel.Debug,
            LogLevel.Warning => SentryLevel.Warning,
            LogLevel.Error => SentryLevel.Error,
            _ => SentryLevel.Info
        };
    }

    public bool IsEnabled(LogLevel logLevel) => true;

    public IDisposable BeginScope<TState>(TState state) where TState : notnull => default!;
}
