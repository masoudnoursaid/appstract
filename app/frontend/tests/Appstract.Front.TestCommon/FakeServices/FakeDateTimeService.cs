using Appstract.Front.Infrastructure.Services.DateTimeService;

namespace Appstract.TestCommon.FakeServices;

public class FakeDateTimeService : IDateTimeService
{
    public DateTime Now { get; private set; } = DateTime.Now;
    public DateTime UtcNow { get; private set; } = DateTime.UtcNow;

    public void SetNow(DateTime now) => Now = now;
    public void SetUtcNow(DateTime utcNow) => UtcNow = utcNow;
}