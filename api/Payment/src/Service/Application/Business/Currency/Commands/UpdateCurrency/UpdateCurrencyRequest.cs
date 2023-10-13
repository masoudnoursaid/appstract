using Application.Business.Currency.Dto;
using ErrorHandling;
using ErrorHandling.Attributes;
using ErrorHandling.Enums;
using MediatR;

namespace Application.Business.Currency.Commands.UpdateCurrency;

[HandlerCode(HandlerCode.UpdateCurrency)]
public record UpdateCurrencyRequest(CurrencyModelDto Dto, string Id) : IRequest<Response>;