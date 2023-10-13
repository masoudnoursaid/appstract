using ErrorHandling;
using ErrorHandling.Attributes;
using ErrorHandling.Enums;
using MediatR;

namespace Application.Business.Currency.Commands.DeleteCurrency;

[HandlerCode(HandlerCode.DeleteCurrency)]
public record DeleteCurrencyRequest(string Id, bool IncludeDependencies = true) : IRequest<Response>;