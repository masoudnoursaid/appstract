using Application.Business.Payer.Queries.PayerList;
using Application.Common.Consts;
using ErrorHandling;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Common.Swagger;
using PayerApiControllerEndPoint = Admin.Api.Constants.EndPoints.PayerApi;

namespace Admin.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route(PayerApiControllerEndPoint.CONTROLLER)]
public class PayerApiController : Controller
{
    private readonly IMediator _mediator;

    public PayerApiController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route(PayerApiControllerEndPoint.LIST)]
    [SwaggerRequestType(typeof(PayerListRequest))]
    public async Task<Response<PayerListDto>> PayerList(int? page, int perPage = Pagination.PER_PAGE)
    {
        var result = await _mediator.Send(new PayerListRequest(perPage, page));
        return result;
    }
}