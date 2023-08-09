using Domain.ValueObjects.IP;

namespace Application.Common.Services;

public interface IIpService
{
    string? GetRawRemoteIpAddress();
    IpAddress GetRemoteIpAddress();
}
