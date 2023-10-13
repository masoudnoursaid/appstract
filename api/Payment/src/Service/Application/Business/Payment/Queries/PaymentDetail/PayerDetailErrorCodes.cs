using ErrorHandling.Attributes;
using ErrorHandling.Enums;

namespace Application.Business.Payment.Queries.PaymentDetail;

[HandlerCode(HandlerCode.PaymentDetail)]
public enum PaymentDetailErrorCodes
{
    PaymentNotFound = 13_05_01
}