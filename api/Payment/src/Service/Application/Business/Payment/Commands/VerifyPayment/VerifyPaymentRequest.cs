using ErrorHandling;
using ErrorHandling.Attributes;
using ErrorHandling.Enums;
using MediatR;

namespace Application.Business.Payment.Commands.VerifyPayment;

[HandlerCode(HandlerCode.VerifyPayment)]
public record VerifyPaymentRequest(string ProvidedPaymentId, string paymentId) : IRequest<Response<VerifyPaymentDto>>;