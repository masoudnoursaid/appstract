using Application.Business.PaymentMethod.Dto;
using ErrorHandling;
using ErrorHandling.Attributes;
using ErrorHandling.Enums;
using MediatR;

namespace Application.Business.PaymentMethod.Commands.UpdatePaymentMethod;

[HandlerCode(HandlerCode.UpdatePaymentMethod)]
public record UpdatePaymentMethodRequest(PaymentMethodModelDto Dto, string Id) : IRequest<Response>;