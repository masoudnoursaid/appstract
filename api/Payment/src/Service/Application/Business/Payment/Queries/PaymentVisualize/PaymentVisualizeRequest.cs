using ErrorHandling;
using ErrorHandling.Attributes;
using ErrorHandling.Enums;
using MediatR;

namespace Application.Business.Payment.Queries.PaymentVisualize;

[HandlerCode(HandlerCode.PaymentVisualize)]
public record PaymentVisualizeRequest(string PaymentId) : IRequest<Response<PaymentVisualizeDto>>;


