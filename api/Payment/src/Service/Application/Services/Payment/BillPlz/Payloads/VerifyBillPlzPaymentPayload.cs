using Application.Common.Payloads;

namespace Application.Services.Payment.BillPlz.Payloads;

public record VerifyBillPlzPaymentPayload(string ProvidedPaymentId) : VerifyPaymentPayload(ProvidedPaymentId);
