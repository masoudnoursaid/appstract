namespace Appstract.Front.Infrastructure.Services.DateTimeService;

public interface IDateTimeService
{
    DateTime Now { get; }
    DateTime UtcNow { get; }
}