using System.Net;
using System.Net.Sockets;
using Domain.Common.BaseTypes;
using Domain.Enums;
using Domain.Exceptions;

namespace Domain.ValueObjects;

public class IpAddress : ValueObject
{
    private IpAddress(string ip, IpType ipType = IpType.V4)
    {
        Ip = ip;
        IpType = ipType;
    }

    public string Ip { get; }
    public IpType IpType { get; }

    /// <summary>
    ///     Static factory method to create an IpAddress from a string.
    /// </summary>
    /// <exception cref="InvalidIpAddressException">Throws if IP format is invalid.</exception>
    public static IpAddress Create(string ip)
    {
        try
        {
            return From(IPAddress.Parse(ip));
        }
        catch (FormatException e)
        {
            throw new InvalidIpAddressException(ip, e);
        }
    }

    public static bool TryCreate(string ip, out IpAddress? ipAddress)
    {
        var isValidIp = IPAddress.TryParse(ip, out var address);

        if (!isValidIp || address is null)
        {
            ipAddress = null;
            return false;
        }

        ipAddress = From(address);
        return true;
    }

    public static IpAddress From(IPAddress address)
    {
        var ipType = address.AddressFamily == AddressFamily.InterNetworkV6 ? IpType.V6 : IpType.V4;
        return new IpAddress(address.ToString(), ipType);
    }

    public override string ToString()
    {
        return Ip;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Ip;
        yield return IpType;
    }
}