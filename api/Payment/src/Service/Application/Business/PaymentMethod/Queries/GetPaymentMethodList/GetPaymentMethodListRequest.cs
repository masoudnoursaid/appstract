using Application.Common.BaseTypes.Request;
using ErrorHandling;
using ErrorHandling.Attributes;
using ErrorHandling.Enums;
using MediatR;

namespace Application.Business.PaymentMethod.Queries.GetPaymentMethodList;

[HandlerCode(HandlerCode.GetPaymentMethodList)]
public record GetPaymentMethodListRequest : GetListRequest, IRequest<Response<PaymentMethodList>>
{
    public GetPaymentMethodListRequest(int perPage, int? page) : base(perPage, page)
    {
    }
}