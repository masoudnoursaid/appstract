using ErrorHandling.Attributes;
using ErrorHandling.Enums;

namespace Application.Payment.Commands.CreatePayment;

[HandlerCode(HandlerCode.CreatePayment)]
public enum CreatePaymentErrorCodes
{
    PayHubFailed = 12_01_01
}