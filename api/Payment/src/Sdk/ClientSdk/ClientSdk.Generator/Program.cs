using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ClientSdk.Generator;

public class Program
{
    public static async Task Main(string[] args)
    {
        using var host = Host.CreateDefaultBuilder(args)
            .ConfigureHostConfiguration(builder =>
            {
                builder.AddEnvironmentVariables();
                builder.AddCommandLine(args);
            })
            .ConfigureServices((_, services) =>
            {
                services.AddLogging();
                services.AddHttpClient();
            })
            .Build();
        ILogger logger = host.Services.GetRequiredService<ILogger<Program>>();


        await new SdkGenerator<Admin.Api.Program>(SdkGeneratorConstants.ADMIN_API_SDK_PROJECT_NAME, logger, args)
            .SetupSdk();

        await new SdkGenerator<Payment.Api.Program>(SdkGeneratorConstants.PAYMENT_API_SDK_PROJECT_NAME, logger, args)
            .SetupSdk();
    }
}