using ErrorHandling.Attributes;
using ErrorHandling.Enums;

namespace Application.Business.PaymentMethod.Commands.UpdatePaymentMethod;

[HandlerCode(HandlerCode.UpdatePaymentMethod)]
public enum UpdatePaymentMethodErrorCodes
{
    PaymentMethodNotFound = 11_03_01
}