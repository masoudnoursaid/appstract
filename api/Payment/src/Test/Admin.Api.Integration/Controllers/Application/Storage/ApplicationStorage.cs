using ClientSdk.AdminApi.V1;

namespace Admin.Api.Integration.Controllers.Application.Storage;

public static class ApplicationStorage
{
    public static IEnumerable<TestCaseData> ApplicationTestCases(string title)
    {
        yield return new TestCaseData(new RegisterApplicationRequest
        {
            Title = title,
            AuthorizedIpAddresses = new List<string> { "127.0.0.1" }
        });
    }
}