using Application.Business.PaymentStatus.Queries.PaymentStatusList;
using Application.Common.Consts;
using ErrorHandling;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Common.Swagger;
using PaymentStatusApiControllerEndPoint = Admin.Api.Constants.EndPoints.PaymentStatusApi;

namespace Admin.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route(PaymentStatusApiControllerEndPoint.CONTROLLER)]
public class PaymentStatusApiController : Controller
{
    private readonly IMediator _mediator;

    public PaymentStatusApiController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route(PaymentStatusApiControllerEndPoint.LIST)]
    [SwaggerRequestType(typeof(PaymentStatusListRequest))]
    public async Task<Response<PaymentStatusListDto>> PaymentStatusList(int? page, int perPage = Pagination.PER_PAGE)
    {
        var result = await _mediator.Send(new PaymentStatusListRequest(perPage, page));
        return result;
    }
}