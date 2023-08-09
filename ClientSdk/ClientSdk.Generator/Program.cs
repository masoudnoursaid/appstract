using ClientSdk.Generator;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using IHost host = Host.CreateDefaultBuilder(args)
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

string defaultOutputPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..");
string outputPath = (args.Length >= 2 ? args[0] : defaultOutputPath) ??
                    throw new InvalidOperationException("Output path is not specified.");

using  WebApplicationFactory<SimpleApi.Program> webApplicationFactory = new();
HttpClient httpClient = webApplicationFactory.CreateClient();

await GenerateMobileSdk("1");
await GenerateWebSdk("1");

async Task GenerateMobileSdk(string version)
{
    await GenerateSdk(
        $"CustomerMobileV{version}",
        SdkGeneratorConstants.MOBILE_PROJECT_NAME,
        SdkGeneratorConstants.MOBILE_PROJECT_NAMESPACE.Replace("{Version}", version),
        SdkGeneratorConstants.MOBILE_SWAGGER_ENDPOINT.Replace("{Version}", version));
}

async Task GenerateWebSdk(string version)
{
    await GenerateSdk(
        $"CustomerWebV{version}",
        SdkGeneratorConstants.WEB_PROJECT_NAME,
        SdkGeneratorConstants.WEB_PROJECT_NAMESPACE.Replace("{Version}", "1"),
        SdkGeneratorConstants.WEB_SWAGGER_ENDPOINT.Replace("{Version}", "1"));
}

async Task GenerateSdk(string name, string project, string @namespace, string path)
{
    string swaggerJson = await httpClient.GetStringAsync(path);

    SdkCodeGenerator sdkCodeGenerator = new();
    string code = await sdkCodeGenerator.Generate(@namespace, swaggerJson);

    string projectPath = Path.Combine(outputPath, project);

    if (!Directory.Exists(projectPath))
    {
        Directory.CreateDirectory(projectPath);
        logger.LogInformation("Output directory '{OutputPath}' created", projectPath);
    }

    File.WriteAllText(Path.Combine(projectPath, $"{name}Client.g.cs"), code);
    logger.LogInformation("File '{FileName}' generated", $"{name}Client.g.cs");
}

logger.LogInformation("SDK generated successfully");