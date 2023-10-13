using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;

namespace ClientSdk.Generator;

public class SdkGenerator<TStartup> where TStartup : class
{
    private readonly ILogger _logger;
    private readonly ProjectModel _model;
    private readonly string _outputPath;
    private HttpClient? client;

    public SdkGenerator(string projectName, ILogger logger, string[] args)
    {
        _logger = logger;
        _model = ProjectStorage.Get(projectName) ?? throw new ArgumentException($"{projectName} not found");
        var defaultOutputPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..");
        _outputPath = (args.Length >= 2 ? args[0] : defaultOutputPath) ??
                      throw new InvalidOperationException("Output path is not specified.");
    }


    public async Task SetupSdk()
    {
        using WebApplicationFactory<TStartup> factory = new();
        client = factory.CreateClient();
        await GenerateSdk("1");
        _logger.LogInformation("SDK generated successfully");
    }


    private async Task GenerateSdk(string version)
    {
        await GenerateSdk(
            $"{_model.Name}V{version}",
            _model.Name,
            _model.NameSpace.Replace("{Version}", "1"),
            _model.SwaggerEndPoint.Replace("{Version}", "1"));
    }

    private async Task GenerateSdk(string name, string project, string @namespace, string path)
    {
        var swaggerJson = await client!.GetStringAsync(path);

        SdkCodeGenerator sdkCodeGenerator = new();
        var code = await sdkCodeGenerator.Generate(@namespace, swaggerJson);

        var projectPath = Path.Combine(_outputPath, project);

        if (!Directory.Exists(projectPath))
        {
            Directory.CreateDirectory(projectPath);
            _logger.LogInformation("Output directory '{OutputPath}' created", projectPath);
        }

        File.WriteAllText(Path.Combine(projectPath, $"{name}Client.g.cs"), code);
        _logger.LogInformation("File '{FileName}' generated", $"{name}Client.g.cs");
    }
}