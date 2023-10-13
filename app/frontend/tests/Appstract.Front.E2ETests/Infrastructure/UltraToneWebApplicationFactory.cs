using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace Appstract.Web.E2ETests.Infrastructure;

public class UltraToneWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>
    where TProgram : class
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        IHost testHost = base.CreateHost(builder);
        builder.ConfigureWebHost(webHostBuilder => webHostBuilder.UseKestrel());

        IHost host = builder.Build();
        host.Start();

        return new CompositeHost(testHost, host);
    }
}