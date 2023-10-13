using Infrastructure.Persistence.Sql.Seeds;
using Web.Common.Extensions;
using Web.Common.Utilities;

namespace Admin.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        EnvUtils.SetupEnvFile();

        var host = CreateNewHostBuilder(args).Build();

        if (args.Any(a => a.Equals("seed", StringComparison.OrdinalIgnoreCase)))
        {
            using var scope = host.Services.CreateScope();
            var provider = scope.ServiceProvider;

            var seedService = provider.GetRequiredService<ISeedService>();
            await seedService.StartAll();
        }


        await host.RunAsync();
    }


    private static IHostBuilder CreateNewHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
            .SetupSerilogWithSentry();
    }
}