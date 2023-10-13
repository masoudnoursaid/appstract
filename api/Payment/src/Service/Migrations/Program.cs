using Infrastructure.Common;
using Infrastructure.DependencyInjection;
using Infrastructure.Persistence.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Migrations;

public class Program
{
    public static void Main(string[] args)
    {
        var envFilePath = Path.Combine(Directory.GetCurrentDirectory(), ".env");
        DotEnv.Load(envFilePath);


        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddLogging()
                    .AddDbContext(hostContext.Configuration);
            })
            .Build();

        switch (args.Length)
        {
            case > 0 when args[0] == "migrate":
            {
                using var scope = host.Services.CreateScope();

                var appDb = scope.ServiceProvider.GetRequiredService<PaymentDbContext>();
                appDb.Database.Migrate();
                appDb.Database.EnsureCreated();
                return;
            }
            case > 0 when args[0] == "digrate":
            {
                using var scope = host.Services.CreateScope();

                var appDb = scope.ServiceProvider.GetRequiredService<PaymentDbContext>();
                appDb.Database.EnsureDeleted();
                appDb.Database.Migrate();
                appDb.Database.EnsureCreated();
                return;
            }
            default:
                Console.WriteLine(@"No arguments provided. Running host...");
                host.Run();
                break;
        }
    }
}