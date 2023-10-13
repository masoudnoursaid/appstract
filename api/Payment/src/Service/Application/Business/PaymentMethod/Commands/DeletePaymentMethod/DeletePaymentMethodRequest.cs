using ErrorHandling;
using ErrorHandling.Attributes;
using ErrorHandling.Enums;
using MediatR;

namespace Application.Business.PaymentMethod.Commands.DeletePaymentMethod;

[HandlerCode(HandlerCode.DeletePaymentMethod)]
public record DeletePaymentMethodRequest(string Id, bool IncludeDependencies = true) : IRequest<Response>;