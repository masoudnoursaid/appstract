using Application.Services.Payment.Mollie.Payloads;
using Application.Settings.Environments.GlobalPaymentApi;
using Infrastructure.Services.Mollie.Model;
using Mollie.Api.Models;
using Mollie.Api.Models.Payment.Request;
using Mollie.Api.Models.Payment.Response;

namespace Infrastructure.Services.Mollie.Extension;

public static class MollieMapperExtension
{
    public static PaymentRequest ToPaymentRequest(this MollieCreatePaymentModel model)
        => new()
        {
            Amount = new Amount(model.Currency, decimal.Parse(model.Amount)),
            Description = model.Description,
            CancelUrl = model.CancelUrl,
            RedirectUrl = model.RedirectUrl,
#if !DEBUG
            WebhookUrl = model.WebhookUrl,
#endif
        };


    public static MollieCreatePaymentResponseModel ToCreatePaymentResponse(this PaymentResponse model)
        => new()
        {
            Id = model.Id,
            OrderId = model.OrderId,
            Resource = model.Resource,
            Amount = model.Amount.Value,
            Currency = model.Amount.Currency,
            Checkout = model.Links.Checkout.Href,
            Mode = model.Mode,
            CreatedAt = model.CreatedAt,
            FailedAt = model.FailedAt,
            PaidAt = model.PaidAt
        };


    public static MollieCreatePaymentModel ToMollieCreatePayment(this CreateMolliePaymentPayload model,
        IGlobalPaymentApiEnvironmentSetting setting, bool testMode = true)
        => new()
        {
            Amount = model.Amount.ToString("#.##"),
            CancelUrl = setting.MollieUri.Cancel + $"?paymentId={model.PaymentId}",
            RedirectUrl = setting.MollieUri.RedirectVerify + $"?paymentId={model.PaymentId}",
            WebhookUrl = setting.MollieUri.CallBackVerify.ToString(),
            Description = model.Description,
            Currency = model.Currency,
            TestMode = testMode
        };
}