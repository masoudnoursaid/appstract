using Admin.Api.Integration.Controllers.Currency.Storage;
using ClientSdk.AdminApi.V1;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Admin.Api.Integration.Controllers.Currency;

[TestFixture]
public class CurrencyTest : WebApplicationFactory<Program>
{
    private readonly ICurrencyApiClient _currencyApiClient;

    public CurrencyTest()
    {
        var httpClient = CreateClient();
        _currencyApiClient = new CurrencyApiClient(httpClient);
    }


    [TestCaseSource(typeof(CurrencyStorage), nameof(CurrencyStorage.CurrencyTestCases),
        new object[] { "$", "USD", "US dollar" })]
    [TestCaseSource(typeof(CurrencyStorage), nameof(CurrencyStorage.CurrencyTestCases),
        new object[] { "€", "EUR", "Euro" })]
    public async Task Create_new_currency_should_work_without_problem(CurrencyModelDto dto)
    {
        var result = await _currencyApiClient.CreateAsync(new CreateCurrencyRequest
        {
            Dto = dto
        });
        Assert.That(result.Success, Is.True);
    }

    [Test]
    public async Task Currency_list_has_more_then_zero_valid_item()
    {
        var result = await _currencyApiClient.ListAsync(null, null);
        Assert.That(result.Data.Dto.Count, Is.GreaterThan(0));
    }

    [TestCaseSource(typeof(CurrencyStorage), nameof(CurrencyStorage.CurrencyTestCases),
        new object[] { "$-edited", "USD-edited", "US dollar" })]
    [TestCaseSource(typeof(CurrencyStorage), nameof(CurrencyStorage.CurrencyTestCases),
        new object[] { "€-edited", "EUR-edited", "Euro" })]
    public async Task Update_first_currency_should_work_without_problem(CurrencyModelDto dto)
    {
        var response = await _currencyApiClient.ListAsync(null, null);
        var first = response.Data.Dto.First();
        var request = new UpdateCurrencyRequest
        {
            Dto = dto,
            Id = first.Id
        };

        var result = await _currencyApiClient.UpdateAsync(request);

        Assert.That(result.Success, Is.True);
    }

    [Test]
    public async Task Delete_first_currency_should_work_without_problem()
    {
        var response = await _currencyApiClient.ListAsync(null, null);
        var targetId = response.Data.Dto.First().Id;

        var result = await _currencyApiClient.DeleteAsync(targetId, false);

        Assert.That(result.Success, Is.True);
    }
}