using Microsoft.Extensions.Logging;

namespace Appstract.TestCommon.Base;

public class TestLogger : ILogger
{
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        string message = formatter(state, exception) + " - " + exception?.Message;
        Console.WriteLine(message);
    }

    public bool IsEnabled(LogLevel logLevel) => true;

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => default;
}