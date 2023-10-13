using System.Net.NetworkInformation;
using Microsoft.Extensions.Logging;

namespace Payment.Common.SDK.Service.Ping;

public interface IPingService
{
    Task<IPStatus> PingServer(Uri uri);
}

public class PingService : IPingService
{
    private readonly ILogger<PingService> _logger;

    public PingService(ILogger<PingService> logger)
    {
        _logger = logger;
    }

    public async Task<IPStatus> PingServer(Uri uri)
    {
        PingReply? reply = null;
        try
        {
            using System.Net.NetworkInformation.Ping ping = new();
            reply = await ping.SendPingAsync(uri.Host, 10);
            _logger.LogDebug(
                $"CONNECTOR_SERVICE.PING --- Ping to {uri.Host}/{reply.Address} --- Reply status : {reply.Status} --- Reply time : {reply.RoundtripTime}");
            return reply.Status;
        }
        catch (Exception e)
        {
            _logger.LogError(e,
                $"CONNECTOR_SERVICE.PING --- Ping to {uri.Host}/{reply?.Address} --- Reply status : {reply?.Status}");
            return IPStatus.TimedOut;
        }
    }
}