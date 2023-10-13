using Application.Business.Payment.Queries.PaymentDetail;
using Application.Business.Payment.Queries.PaymentList;
using Application.Common.Consts;
using ErrorHandling;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Common.Swagger;
using PaymentApiControllerEndPoint = Admin.Api.Constants.EndPoints.PaymentApi;

namespace Admin.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route(PaymentApiControllerEndPoint.CONTROLLER)]
public class PaymentApiController : Controller
{
    private readonly IMediator _mediator;

    public PaymentApiController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route(PaymentApiControllerEndPoint.LIST)]
    [SwaggerRequestType(typeof(PaymentListRequest))]
    public async Task<Response<PaymentListDto>> PaymentList(int? page, int perPage = Pagination.PER_PAGE)
    {
        var result = await _mediator.Send(new PaymentListRequest(perPage, page));
        return result;
    }

    [HttpGet]
    [Route(PaymentApiControllerEndPoint.GET)]
    [SwaggerRequestType(typeof(PaymentDetailRequest))]
    public async Task<Response<PaymentDetailDto>> PaymentDetail(string id)
    {
        var result = await _mediator.Send(new PaymentDetailRequest(id));
        return result;
    }
}