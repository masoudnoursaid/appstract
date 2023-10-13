using ClientSdk.PaymentApi.V1;
using ErrorHandling;
using ErrorHandling.Attributes;
using ErrorHandling.Enums;
using MediatR;

namespace Application.Payment.Commands.CreatePayment;

[HandlerCode(HandlerCode.CreatePayment)]
public record CreatePaymentRequest
    (CreatePaymentPayload Payload, string PaymentMethodId) : IRequest<Response<CreatePaymentDto>>;