using Admin.Api.Integration.Controllers.Application.Storage;
using ClientSdk.AdminApi.V1;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Admin.Api.Integration.Controllers.Application;

[TestFixture]
public class ApplicationTest : WebApplicationFactory<Program>
{
    private readonly IApplicationApiClient _applicationApiClient;

    public ApplicationTest()
    {
        var httpClient = CreateClient();
        _applicationApiClient = new ApplicationApiClient(httpClient);
    }


    [TestCaseSource(typeof(ApplicationStorage), nameof(ApplicationStorage.ApplicationTestCases),
        new object[] { "Test Dynamic" })]
    [TestCaseSource(typeof(ApplicationStorage), nameof(ApplicationStorage.ApplicationTestCases),
        new object[] { "Payment" })]
    public async Task Generate_new_application_should_return_valid_api_key_and_secret(RegisterApplicationRequest dto)
    {
        var result = await _applicationApiClient.CreateAsync(dto);
        Assert.That(result.Success, Is.True);
    }

    [Test]
    public async Task Currency_list_has_more_then_zero_valid_item()
    {
        var result = await _applicationApiClient.ListAsync(null, null);
        Assert.That(result.Data.Applications.Count, Is.GreaterThan(0));
    }
}