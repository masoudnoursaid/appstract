using Infrastructure.Common;
using Infrastructure.Persistence.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Infrastructure.Persistence.Sql.Registration;


namespace Migrations;

public class Program
{
    public static void Main(string[] args)
    {
        var envFilePath = Path.Combine(Directory.GetCurrentDirectory(), ".env");
        DotEnv.Load(envFilePath);


        IHost host = Host.CreateDefaultBuilder(args)
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
                using IServiceScope scope = host.Services.CreateScope();

                var appDb = scope.ServiceProvider.GetRequiredService<AppstractDbContext>();
                appDb.Database.Migrate();
                appDb.Database.EnsureCreated();
                return;
            }
            case > 0 when args[0] == "digrate":
            {
                using IServiceScope scope = host.Services.CreateScope();

                var appDb = scope.ServiceProvider.GetRequiredService<AppstractDbContext>();
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