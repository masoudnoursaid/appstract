using Application.Common.Payloads;

namespace Application.Services.Payment.Mollie.Payloads;

public record VerifyMolliePaymentPayload(string ProvidedPaymentId) : VerifyPaymentPayload(ProvidedPaymentId);
