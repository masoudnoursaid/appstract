using Application.Common.BaseTypes.Request;
using Application.Common.Consts;
using ErrorHandling;
using ErrorHandling.Attributes;
using ErrorHandling.Enums;
using MediatR;

namespace Application.Business.Payment.Queries.PaymentList;

[HandlerCode(HandlerCode.PaymentList)]
public record PaymentListRequest : GetListRequest, IRequest<Response<PaymentListDto>>
{
    public PaymentListRequest(int perPage = Pagination.PER_PAGE, int? page = null) : base(perPage, page)
    {
    }
}