using Application.Common.BaseTypes.Request;
using ErrorHandling;
using ErrorHandling.Attributes;
using ErrorHandling.Enums;
using MediatR;

namespace Application.Business.Applications.Queries.ApplicationList;

[HandlerCode(HandlerCode.GetApplicationsList)]
public record ApplicationListRequest : GetListRequest, IRequest<Response<ApplicationsList>>
{
    public ApplicationListRequest(int perPage, int? page) : base(perPage, page)
    {
    }
}