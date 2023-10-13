using ErrorHandling.Attributes;
using ErrorHandling.Enums;

namespace Application.Business.Payment.Commands.VerifyPayment;

[HandlerCode(HandlerCode.VerifyPayment)]
public enum VerifyPaymentErrorCodes
{
    PaymentNotFound = 13_02_01,
    InvalidProvidedPaymentId = 13_02_02
}