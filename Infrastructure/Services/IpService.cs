using Application.Common.Services;
using Domain.Exceptions;
using Domain.ValueObjects.IP;

namespace Infrastructure.Services;

public class IpService : IIpService
{
    public string? GetRawRemoteIpAddress()
    {
        // ToDo: Implement this using HttpContextAccessor
        return "127.0.0.1";
    }

    public IpAddress GetRemoteIpAddress()
    {
        string? ip = GetRawRemoteIpAddress();
        IpAddress? ipAddress = null;
        bool isValidIp = !string.IsNullOrWhiteSpace(ip) && IpAddress.TryCreate(ip, out ipAddress);

        if (isValidIp is false || ipAddress is null)
        {
            throw new InvalidIpAddressException(ip);
        }

        return ipAddress;
    }
}