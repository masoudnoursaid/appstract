using Domain.ValueObjects;

namespace Application.Services.IP;

public interface IIpService
{
    string? GetRawRemoteIpAddress();
    IpAddress GetRemoteIpAddress();
    string GetRegion();
}