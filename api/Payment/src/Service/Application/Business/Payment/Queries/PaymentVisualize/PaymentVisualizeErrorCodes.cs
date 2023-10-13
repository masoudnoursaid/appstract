using ErrorHandling.Attributes;
using ErrorHandling.Enums;

namespace Application.Business.Payment.Queries.PaymentVisualize;

[HandlerCode(HandlerCode.PaymentVisualize)]
public enum PaymentVisualizeErrorCodes
{
    PaymentNotFound = 13_06_01
}