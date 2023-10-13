using Web.Common.Extensions;
using Web.Common.Utilities;

namespace Payment.Api;

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