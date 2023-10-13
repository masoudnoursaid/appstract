using Application.Common.Payloads;
using ErrorHandling;
using ErrorHandling.Attributes;
using ErrorHandling.Enums;
using MediatR;

namespace Application.Business.Payment.Commands.CreatePayment;

[HandlerCode(HandlerCode.CreatePayment)]
public record CreatePaymentRequest
    (CreatePaymentPayload Payload, string PaymentMethodId) : IRequest<Response<CreatePaymentDto>>;