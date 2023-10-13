namespace Application.Common.Payloads;

public abstract record VerifyPaymentPayload(string ProvidedPaymentId) : IPayload;
