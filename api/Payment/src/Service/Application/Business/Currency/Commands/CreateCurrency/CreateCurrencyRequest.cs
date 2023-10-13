using Application.Business.Currency.Dto;
using ErrorHandling;
using ErrorHandling.Attributes;
using ErrorHandling.Enums;
using MediatR;

namespace Application.Business.Currency.Commands.CreateCurrency;

[HandlerCode(HandlerCode.CreateCurrency)]
public record CreateCurrencyRequest(CurrencyModelDto Dto) : IRequest<Response>;