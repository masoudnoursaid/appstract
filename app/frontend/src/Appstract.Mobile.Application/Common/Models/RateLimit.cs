namespace Appstract.Mobile.Application.Common.Models;

public class RateLimit
{
    public int Count { get; set; }
    public int Period { get; set; }
    public int MinimumInterval { get; set; }
}
