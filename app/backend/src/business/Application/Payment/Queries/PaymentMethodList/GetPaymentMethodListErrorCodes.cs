using ErrorHandling.Attributes;
using ErrorHandling.Enums;

namespace Application.Payment.Queries.PaymentMethodList;

[HandlerCode(HandlerCode.GetPaymentMethodList)]
public enum GetPaymentMethodListErrorCodes
{
    PayHubGetMethodListFailed = 12_02_01
}