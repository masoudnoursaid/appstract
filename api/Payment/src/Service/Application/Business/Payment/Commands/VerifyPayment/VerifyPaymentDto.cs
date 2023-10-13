namespace Application.Business.Payment.Commands.VerifyPayment;

public record VerifyPaymentDto(string PaymentId
    , string ProvidedPaymentId
    , string Status
    , string InvoiceNumber
    , string MethodName
    , string Currency
    , float Amount);