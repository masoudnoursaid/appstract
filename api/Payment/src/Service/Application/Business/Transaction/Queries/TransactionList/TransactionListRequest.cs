using Application.Common.BaseTypes.Request;
using Application.Common.Consts;
using ErrorHandling;
using ErrorHandling.Attributes;
using ErrorHandling.Enums;
using MediatR;

namespace Application.Business.Transaction.Queries.TransactionList;

[HandlerCode(HandlerCode.TransactionList)]
public record TransactionListRequest : GetListRequest, IRequest<Response<TransactionListDto>>
{
    public TransactionListRequest(int perPage = Pagination.PER_PAGE, int? page = null) : base(perPage, page)
    {
    }
}