using Appstract.Web.Extensions;
using Infrastructure.Common;

namespace Appstract.WebApi;

public class Program
{
    public static async Task Main(string[] args)
    {
        EnvUtils.SetupEnvFile();

        var host = CreateNewHostBuilder(args).Build();

        await host.RunAsync();
    }


    private static IHostBuilder CreateNewHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
            .SetupSerilogWithSentry();
    }
}