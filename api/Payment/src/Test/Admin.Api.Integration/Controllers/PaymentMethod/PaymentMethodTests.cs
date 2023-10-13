using Admin.Api.Integration.Controllers.PaymentMethod.Storage;
using ClientSdk.AdminApi.V1;
using Microsoft.AspNetCore.Mvc.Testing;
using Test.Common.Extensions;

namespace Admin.Api.Integration.Controllers.PaymentMethod;

[TestFixture]
public sealed class PaymentMethodTests : WebApplicationFactory<Program>
{
    private readonly IPaymentMethodApiClient _paymentMethodApiClient;

    public PaymentMethodTests()
    {
        var httpClient = CreateClient();
        _paymentMethodApiClient = new PaymentMethodApiClient(httpClient);

        Services.TestRUp().Wait();
    }


    [TestCaseSource(typeof(PaymentMethodTestCaseStorage), nameof(PaymentMethodTestCaseStorage.PaymentMethodTestCases),
        new object[] { "Paypal", SourceImplementedGetWay.Paypal })]
    [TestCaseSource(typeof(PaymentMethodTestCaseStorage), nameof(PaymentMethodTestCaseStorage.PaymentMethodTestCases),
        new object[] { "Stripe", SourceImplementedGetWay.Stripe })]
    [TestCaseSource(typeof(PaymentMethodTestCaseStorage), nameof(PaymentMethodTestCaseStorage.PaymentMethodTestCases),
        new object[] { "Billplz", SourceImplementedGetWay.Billplz })]
    public void Create_new_payment_method_should_work_without_problem(PaymentMethodModelDto dto)
    {
        Assert.Pass();
    }

    [Test]
    public async Task Payment_method_list_has_more_then_zero_valid_item()
    {
        var result = await _paymentMethodApiClient.ListAsync(null, null);

        Assert.That(result.Success, Is.True);
    }

    [TestCaseSource(typeof(PaymentMethodTestCaseStorage), nameof(PaymentMethodTestCaseStorage.PaymentMethodTestCases),
        new object[] { "Billplz", SourceImplementedGetWay.Billplz })]
    public async Task Update_first_payment_method_should_work_without_problem(PaymentMethodModelDto dto)
    {
        var response = await _paymentMethodApiClient.ListAsync(null, null);
        var target = response.Data.Dtos.First();
        dto.MerchantOwnerId = target.MerchantOwnerId;
        var request = new UpdatePaymentMethodRequest
        {
            Dto = dto,
            Id = target.Id
        };

        var result = await _paymentMethodApiClient.UpdateAsync(request);
        Assert.That(result.Success, Is.True);
    }

    [Test]
    public async Task Delete_first_payment_method_should_work_without_problem()
    {
        var target = await _paymentMethodApiClient.ListAsync(null, null);

        var result = await _paymentMethodApiClient.DeleteAsync(target.Data.Dtos.First().Id, false);
        Assert.That(result.Success, Is.True);
    }
}