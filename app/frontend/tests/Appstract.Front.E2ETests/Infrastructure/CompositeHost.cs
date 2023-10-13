using Microsoft.Extensions.Hosting;

namespace Appstract.Web.E2ETests.Infrastructure;

public class CompositeHost : IHost
{
    private readonly IHost _testHost;
    private readonly IHost _kestrelHost;
    public CompositeHost(IHost testHost, IHost kestrelHost)
    {
        _testHost = testHost;
        _kestrelHost = kestrelHost;
    }
    public IServiceProvider Services => _testHost.Services;
    public void Dispose()
    {
        _testHost.Dispose();
        _kestrelHost.Dispose();
    }
    public async Task StartAsync(
      CancellationToken cancellationToken = default)
    {
        await _testHost.StartAsync(cancellationToken);
        await _kestrelHost.StartAsync(cancellationToken);
    }
    public async Task StopAsync(
      CancellationToken cancellationToken = default)
    {
        await _testHost.StopAsync(cancellationToken);
        await _kestrelHost.StopAsync(cancellationToken);
    }
}
