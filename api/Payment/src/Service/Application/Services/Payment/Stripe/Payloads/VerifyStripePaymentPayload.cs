using Application.Common.Payloads;

namespace Application.Services.Payment.Stripe.Payloads;

public record VerifyStripePaymentPayload(string ProvidedPaymentId) : VerifyPaymentPayload(ProvidedPaymentId);
