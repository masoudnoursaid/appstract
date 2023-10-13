using ClientSdk.AdminApi.V1;

namespace Admin.Api.Integration.Controllers.Currency.Storage;

public static class CurrencyStorage
{
    public static IEnumerable<TestCaseData> CurrencyTestCases(string symbol, string title, string description)
    {
        yield return new TestCaseData(new CurrencyModelDto
        {
            Symbol = symbol,
            Title = $"{title}-{Guid.NewGuid()}",
            FullName = $"{description}-{Guid.NewGuid()}"
        });
    }
}