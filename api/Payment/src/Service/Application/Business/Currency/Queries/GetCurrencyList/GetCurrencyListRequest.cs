using Application.Common.BaseTypes.Request;
using ErrorHandling;
using ErrorHandling.Attributes;
using ErrorHandling.Enums;
using MediatR;

namespace Application.Business.Currency.Queries.GetCurrencyList;

[HandlerCode(HandlerCode.GetCurrencyList)]
public record GetCurrencyListRequest : GetListRequest, IRequest<Response<CurrencyList>>
{
    public GetCurrencyListRequest(int perPage, int? page) : base(perPage, page)
    {
    }
}