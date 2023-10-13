using Application.Common.BaseTypes.Request;
using ErrorHandling;
using ErrorHandling.Attributes;
using ErrorHandling.Enums;
using MediatR;

namespace Application.Business.PaymentStatus.Queries.PaymentStatusList;

[HandlerCode(HandlerCode.PaymentStatusList)]
public record PaymentStatusListRequest : GetListRequest, IRequest<Response<PaymentStatusListDto>>
{
    public PaymentStatusListRequest(int perPage, int? page) : base(perPage, page)
    {
    }
}