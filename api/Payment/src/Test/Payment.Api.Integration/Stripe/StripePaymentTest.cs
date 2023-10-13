using Application.Common.Regex;
using ClientSdk.PaymentApi.V1;
using Infrastructure.Persistence.Sql.Seeds;
using Microsoft.AspNetCore.Mvc.Testing;
using Payment.Api.Integration.Storage;
using Payment.Api.Integration.Stripe.Context;
using Test.Common.Extensions;

namespace Payment.Api.Integration.Stripe;

[TestFixture]
public sealed class StripePaymentTest : WebApplicationFactory<Program>
{
    [SetUp]
    public void ClientCleanUp()
    {
        _paymentClient.DefaultRequestHeaders.Clear();
        _paymentMethodClient.DefaultRequestHeaders.Clear();
    }

    private readonly IPaymentApiClient _paymentApiClient;
    private readonly IStripeTestFixtureContext _fixtureContext;
    private readonly IPaymentMethodApiClient _paymentMethodApiClient;
    private readonly HttpClient _paymentMethodClient;
    private readonly HttpClient _paymentClient;

    public StripePaymentTest()
    {
        Services.TestRUp().Wait();

        _paymentMethodClient = CreateClient();
        _paymentMethodApiClient = new PaymentMethodApiClient(null!,
            ConfigurationStorage.PaymentConfiguration(_paymentMethodClient.BaseAddress!.ToString()),
            _paymentMethodClient);

        _paymentClient = CreateClient();
        _paymentApiClient = new PaymentApiClient(null!,
            ConfigurationStorage.PaymentConfiguration(_paymentClient.BaseAddress!.ToString()), _paymentClient);
        _fixtureContext = new StripeTestFixtureContext();

        SettingUp().Wait();
    }

    public async Task SettingUp()
    {
        var paymentMethods = await _paymentMethodApiClient.ListAsync(null, null, ApplicationSeedStorage.Key.Value);
        var paypalId = paymentMethods.Data.Dtos
            .Single(pm => pm.GetWay == SourceImplementedGetWay.Stripe).Id;
        _fixtureContext.SetMethodId(paypalId);
    }


    [TestCaseSource(typeof(TestCaseStorage), nameof(TestCaseStorage.CreatePaymentCases), new object[] { "USD" })]
    [TestCaseSource(typeof(TestCaseStorage), nameof(TestCaseStorage.CreatePaymentCases), new object[] { "EUR" })]
    public async Task Create_payment_intent_with_stripe_should_return_valid_payment_info(CreatePaymentRequest request)
    {
        request.PaymentMethodId = _fixtureContext.MethodId;
        var result = await _paymentApiClient.CreateAsync(ApplicationSeedStorage.Key.Value, request);

        Assert.That(result.Data.PaymentUrl, Does.Match(ValidUrlRegex.HttpsUrl));
        Assert.That(result.Data.PaymentId, Does.Match(ValidUUIDRegex.UUID));
        Assert.That(result.Data.ProvidedId, Is.Not.Null | Is.Not.Empty);

        await TestContext.Out.WriteLineAsync(
            $"STRIPE_PAYMENT_TEST --- Currency : {request.Payload.Currency} --- Pay Url : {result.Data.PaymentUrl}");
    }
}