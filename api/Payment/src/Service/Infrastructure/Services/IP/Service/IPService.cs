using Application.Services.IP;
using Domain.Exceptions;
using Domain.ValueObjects;

namespace Infrastructure.Services.IP.Service;

public class IpService : IIpService
{
    public string GetRawRemoteIpAddress()
    {
        return "127.0.0.1";
    }

    public IpAddress GetRemoteIpAddress()
    {
        var ip = GetRawRemoteIpAddress();
        IpAddress? ipAddress = null;
        var isValidIp = !string.IsNullOrWhiteSpace(ip) && IpAddress.TryCreate(ip, out ipAddress);

        if (isValidIp is false || ipAddress is null) throw new InvalidIpAddressException(ip);

        return ipAddress;
    }

    public string GetRegion()
    {
        return "NL";
    }
}