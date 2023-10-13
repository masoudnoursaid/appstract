using Appstract.Mobile.Application.Common.Models;

namespace Appstract.Mobile.Application.Dto.Authentication;

public class PhoneOtpDto
{
    public int RequestsCount { get; set; }
    public int Ttl { get; set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public RateLimit? RateLimit { get; set; }
}
