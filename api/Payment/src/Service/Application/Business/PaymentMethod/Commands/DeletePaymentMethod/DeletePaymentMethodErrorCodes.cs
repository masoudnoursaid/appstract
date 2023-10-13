using ErrorHandling.Attributes;
using ErrorHandling.Enums;

namespace Application.Business.PaymentMethod.Commands.DeletePaymentMethod;

[HandlerCode(HandlerCode.DeletePaymentMethod)]
public enum DeletePaymentMethodErrorCodes
{
    PaymentMethodNotFound = 11_02_01
}