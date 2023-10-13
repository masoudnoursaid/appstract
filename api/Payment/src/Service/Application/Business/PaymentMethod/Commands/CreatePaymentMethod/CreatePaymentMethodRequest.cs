using Application.Business.PaymentMethod.Dto;
using ErrorHandling;
using ErrorHandling.Attributes;
using ErrorHandling.Enums;
using MediatR;

namespace Application.Business.PaymentMethod.Commands.CreatePaymentMethod;

[HandlerCode(HandlerCode.CreatePaymentMethod)]
public record CreatePaymentMethodRequest(PaymentMethodModelDto Dto) : IRequest<Response>;