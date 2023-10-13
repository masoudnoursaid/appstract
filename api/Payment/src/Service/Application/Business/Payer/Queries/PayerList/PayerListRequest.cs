using Application.Common.BaseTypes.Request;
using Application.Common.Consts;
using ErrorHandling;
using ErrorHandling.Attributes;
using ErrorHandling.Enums;
using MediatR;

namespace Application.Business.Payer.Queries.PayerList;

[HandlerCode(HandlerCode.PayerList)]
public record PayerListRequest : GetListRequest, IRequest<Response<PayerListDto>>
{
    public PayerListRequest(int perPage = Pagination.PER_PAGE, int? page = null) : base(perPage, page)
    {
    }
}