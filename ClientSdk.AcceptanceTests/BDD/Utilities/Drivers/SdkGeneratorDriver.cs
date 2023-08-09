using ClientSdk.AcceptanceTests.BDD.Utilities.Contexts;
using ClientSdk.Generator;
using Microsoft.AspNetCore.Mvc.Testing;
using SimpleApi;

namespace ClientSdk.AcceptanceTests.BDD.Utilities.Drivers;

public class SdkGeneratorDriver
{
    private readonly SdkGeneratorContext _context;
    private readonly HttpClient _customerHttpClient;
    private readonly SdkCodeGenerator _codeGenerator = new();

    public SdkGeneratorDriver(
        WebApplicationFactory<Program> customerWebApplicationFactory,
        SdkGeneratorContext context)
    {
        _context = context;
        _customerHttpClient = customerWebApplicationFactory.CreateClient();
    }

    public async Task GenerateMobileSdkAsync()
    {
        string swaggerJson =
            await _customerHttpClient.GetStringAsync(SdkGeneratorConstants.MOBILE_SWAGGER_ENDPOINT.Replace("{Version}", "1"));
        _context.GeneratedSource =
            await _codeGenerator.Generate(SdkGeneratorConstants.MOBILE_PROJECT_NAMESPACE.Replace("{Version}", "1"), swaggerJson);
    }

    public async Task GenerateWebSdkAsync()
    {
        string swaggerJson = await _customerHttpClient.GetStringAsync(SdkGeneratorConstants.WEB_SWAGGER_ENDPOINT.Replace("{Version}", "1"));
        _context.GeneratedSource =
            await _codeGenerator.Generate(SdkGeneratorConstants.WEB_PROJECT_NAMESPACE.Replace("{Version}", "1"), swaggerJson);
    }
}