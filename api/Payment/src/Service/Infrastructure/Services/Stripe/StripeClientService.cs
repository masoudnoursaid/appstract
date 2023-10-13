using Application.Services.Payment;
using Application.Services.Payment.Stripe;
using Application.Services.Payment.Stripe.Payloads;
using Application.Services.Payment.Stripe.ResponseModel;
using Application.Settings.Environments.GlobalPaymentApi;
using Application.Settings.Environments.Stripe;
using Domain.Enums;
using Domain.ValueObjects.Payment;
using Infrastructure.Services.Stripe.Context;
using Infrastructure.Services.Stripe.Extension;
using Stripe;
using Stripe.Checkout;

namespace Infrastructure.Services.Stripe;

public class StripeClientService : PaymentClientService<StripeContext>, IStripeClientService
{
    private readonly IGlobalPaymentApiEnvironmentSetting _globalPaymentApiEnvironmentSetting;
    private readonly IStripeEnvironmentSetting _stripeEnvironmentSetting;

    public StripeClientService(IStripeEnvironmentSetting stripeEnvironmentSetting,
        IGlobalPaymentApiEnvironmentSetting globalPaymentApiEnvironmentSetting)
    {
        _stripeEnvironmentSetting = stripeEnvironmentSetting;
        _globalPaymentApiEnvironmentSetting = globalPaymentApiEnvironmentSetting;
    }

    public SourceImplementedGetWay MyGetWayType { get; } = SourceImplementedGetWay.Stripe;

    public async Task<CreateStripePaymentResponseModel> GeneratePaymentUrl(CreateStripePaymentPayload payload,
        bool useSandbox = true)
    {
        var context = useSandbox ? SandBoxConfiguration() : LiveConfiguration();
        var session = await CreateCheckoutSession(context,
            Instance(payload.PaymentId, payload.Items, payload.Description, payload.Email));

        return new CreateStripePaymentResponseModel(session.Url, session.Id);
    }


    public async Task<VerifyStripePaymentResponseModel> VerifyPayment(VerifyStripePaymentPayload payload,
        bool useSandbox = true)
    {
        var context = useSandbox ? SandBoxConfiguration() : LiveConfiguration();
        var session = await GetSessionDetail(context, payload.ProvidedPaymentId);
        var paymentIntent = await GetPaymentIntentDetail(context, session.PaymentIntentId);
        return new VerifyStripePaymentResponseModel(paymentIntent.CustomerId ?? paymentIntent.Id,
            paymentIntent.IsVerified());
    }


    protected override StripeContext SandBoxConfiguration()
    {
        var context = new StripeContext(_stripeEnvironmentSetting.SandBoxSecretKey!);
        return context;
    }

    protected override StripeContext LiveConfiguration()
    {
        var context = new StripeContext(_stripeEnvironmentSetting.SecretKey!);
        return context;
    }

    private async Task<Session> GetSessionDetail(StripeContext context, string sessionId)
    {
        var sessionService = new SessionService();
        var session = await sessionService.GetAsync(sessionId, requestOptions: context.RequestOptions);
        return session;
    }

    private async Task<PaymentIntent> GetPaymentIntentDetail(StripeContext context, string paymentIntentId)
    {
        var paymentIntentService = new PaymentIntentService();
        var paymentIntent =
            await paymentIntentService.GetAsync(paymentIntentId, requestOptions: context.RequestOptions);
        return paymentIntent;
    }

    private async Task<Session> CreateCheckoutSession(StripeContext context, SessionCreateOptions options)
    {
        var sessionService = new SessionService();
        var result = await sessionService.CreateAsync(options, context.RequestOptions);
        return result;
    }

    public SessionCreateOptions Instance(string paymentId
        , IEnumerable<PaymentItem> items
        , string description
        , string email)
    {
        return new SessionCreateOptions
        {
            LineItems = items.Select(i => new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = long.Parse(i.Price),
                    Currency = i.Currency,
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = i.Name
                    }
                },
                Quantity = i.Quantity
            }).ToList(),
            PaymentIntentData = new SessionPaymentIntentDataOptions
            {
                Description = description
            },
            Mode = "payment",
            CustomerEmail = email,
            SuccessUrl = _globalPaymentApiEnvironmentSetting.StripeUri.RedirectVerify +
                         $"?{nameof(paymentId)}={paymentId}",
            CancelUrl = _globalPaymentApiEnvironmentSetting.StripeUri.Cancel + $"?{nameof(paymentId)}={paymentId}"
        };
    }
}