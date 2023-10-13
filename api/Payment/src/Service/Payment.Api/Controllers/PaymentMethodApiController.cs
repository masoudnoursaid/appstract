using Application.Business.PaymentMethod.Dto;
using Application.Business.PaymentMethod.Queries.GetPaymentMethodList;
using Application.Common.Consts;
using ErrorHandling;
using Infrastructure.Attribute.Authentication.Hmac;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentMethodApiEndPointController = Payment.Api.Constants.EndPoints.PaymentMethodApi;

namespace Payment.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[HmacAuthentication]
[Route(PaymentMethodApiEndPointController.CONTROLLER)]
public class PaymentMethodApiController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentMethodApiController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route(PaymentMethodApiEndPointController.LIST)]
    public async Task<Response<PaymentMethodList>> GetPaymentMethodList(int? page, int? perPage)
    {
        var result = await _mediator.Send(new GetPaymentMethodListRequest(perPage ?? Pagination.PER_PAGE, page));
        return result;
    }

    [HttpGet]
    [Route(PaymentMethodApiEndPointController.GET)]
    public Task<Response<PaymentMethodDto>> GetPaymentMethod(string id)
    {
        throw new NotImplementedException();
    }
}