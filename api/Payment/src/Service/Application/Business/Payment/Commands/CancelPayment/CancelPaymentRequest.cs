using ErrorHandling;
using ErrorHandling.Attributes;
using ErrorHandling.Enums;
using MediatR;

namespace Application.Business.Payment.Commands.CancelPayment;

[HandlerCode(HandlerCode.CancelPayment)]
public record CancelPaymentRequest(string PaymentId) : IRequest<Response>;