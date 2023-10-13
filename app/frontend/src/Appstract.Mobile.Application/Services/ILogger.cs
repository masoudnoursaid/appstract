namespace Appstract.Mobile.Application.Services;

public interface ILogger : Microsoft.Extensions.Logging.ILogger
{
    public void LogException(Exception e);
}