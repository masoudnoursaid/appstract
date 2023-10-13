using ErrorHandling.Attributes;
using ErrorHandling.Enums;

namespace Application.Business.Payment.Commands.CreatePayment;

[HandlerCode(HandlerCode.CreatePayment)]
public enum CreatePaymentErrorCodes
{
    MethodNotFound = 13_01_01,
    CurrencyNotFound = 13_01_02,
    ApplicationNotFound = 13_01_03
}