using Application.Common.BaseTypes.Request;
using Application.Common.Consts;
using ErrorHandling;
using MediatR;

namespace Application.Business.Customer.Queries.CustomerList;

public record CustomerListRequest : GetListRequest, IRequest<Response<CustomerListDto>>
{
    public CustomerListRequest(int perPage = Pagination.PER_PAGE, int? page = null)
    {
    }
}