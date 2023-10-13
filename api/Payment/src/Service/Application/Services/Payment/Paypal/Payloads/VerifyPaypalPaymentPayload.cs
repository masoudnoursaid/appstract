using Application.Common.Payloads;

namespace Application.Services.Payment.Paypal.Payloads;

public record VerifyPaypalPaymentPayload(string ProvidedPaymentId) : VerifyPaymentPayload(ProvidedPaymentId);
