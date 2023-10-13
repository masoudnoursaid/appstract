using Application.Business.Transaction.Queries.TransactionList;
using Application.Common.Consts;
using ErrorHandling;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Common.Swagger;
using TransactionApiControllerEndPoint = Admin.Api.Constants.EndPoints.TransactionApi;

namespace Admin.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route(TransactionApiControllerEndPoint.CONTROLLER)]
public class TransactionApiController : Controller
{
    private readonly IMediator _mediator;

    public TransactionApiController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route(TransactionApiControllerEndPoint.LIST)]
    [SwaggerRequestType(typeof(TransactionListRequest))]
    public async Task<Response<TransactionListDto>> TransactionList(int? page, int perPage = Pagination.PER_PAGE)
    {
        var result = await _mediator.Send(new TransactionListRequest(perPage, page));
        return result;
    }
}