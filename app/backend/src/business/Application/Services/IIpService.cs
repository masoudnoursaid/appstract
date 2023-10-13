using Domain.ValueObjects.IP;

namespace Application.Services;

public interface IIpService
{
    string? GetRawRemoteIpAddress();
    IpAddress GetRemoteIpAddress();
}
