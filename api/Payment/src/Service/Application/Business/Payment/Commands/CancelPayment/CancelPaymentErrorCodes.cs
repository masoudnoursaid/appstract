using ErrorHandling.Attributes;
using ErrorHandling.Enums;

namespace Application.Business.Payment.Commands.CancelPayment;

[HandlerCode(HandlerCode.CancelPayment)]
public enum CancelPaymentErrorCodes
{
    PaymentNotFound = 13_03_01
}