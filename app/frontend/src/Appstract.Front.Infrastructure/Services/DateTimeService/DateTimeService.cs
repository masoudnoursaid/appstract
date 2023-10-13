namespace Appstract.Front.Infrastructure.Services.DateTimeService;

public class DateTimeService : IDateTimeService
{
    public DateTime Now => DateTime.Now;
    public DateTime UtcNow => DateTime.UtcNow;
}