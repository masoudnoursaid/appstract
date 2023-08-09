using System.Security.Cryptography;
using ClientSdk.AcceptanceTests.BDD.Utilities.Contexts;
using ClientSdk.AcceptanceTests.BDD.Utilities.Drivers;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace ClientSdk.AcceptanceTests.BDD.Utilities.Steps;

[Binding]
public class SdkGeneratorSteps
{
    private readonly SdkGeneratorContext _context;
    private readonly SdkGeneratorDriver _driver;

    public SdkGeneratorSteps(SdkGeneratorContext context, SdkGeneratorDriver driver)
    {
        _context = context;
        _driver = driver;
    }

    [When(@"the mobile SDK is generated")]
    public async Task WhenTheMobileSdkIsGenerated()
    {
        await _driver.GenerateMobileSdkAsync();
    }

    [Then(@"the generated SDK source should be equal to content of ""(.*)""")]
    public void ThenTheGeneratedSdkSourceShouldBeEqualToContentOf(string sdkFilePath)
    {
        string expected = CalculateHashFromString(File.ReadAllText(sdkFilePath));
        string actual = CalculateHashFromString(_context.GeneratedSource);

        actual.Should().Be(expected, "The generated SDK is not updated");
    }

    [When(@"the web SDK is generated")]
    public async Task WhenTheWebSdkIsGenerated()
    {
        await _driver.GenerateWebSdkAsync();
    }

    private static string CalculateHashFromString(string inputString)
    {
        using SHA256 sha256 = SHA256.Create();

        return BitConverter.ToString(sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(inputString))).Replace("-", string.Empty);
    }
}