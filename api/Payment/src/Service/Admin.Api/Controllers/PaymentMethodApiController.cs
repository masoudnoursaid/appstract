using Application.Business.PaymentMethod.Commands.CreatePaymentMethod;
using Application.Business.PaymentMethod.Commands.DeletePaymentMethod;
using Application.Business.PaymentMethod.Commands.UpdatePaymentMethod;
using Application.Business.PaymentMethod.Dto;
using Application.Business.PaymentMethod.Queries.GetPaymentMethodList;
using Application.Common.Consts;
using ErrorHandling;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Common.Swagger;
using PaymentMethodApiEndPointController = Admin.Api.Constants.EndPoints.PaymentMethodApi;

namespace Admin.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
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
    [SwaggerRequestType(typeof(GetPaymentMethodListRequest))]
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

    [HttpPut]
    [Route(PaymentMethodApiEndPointController.CREATE)]
    public async Task<Response> CreatePaymentMethod(CreatePaymentMethodRequest request)
    {
        var result = await _mediator.Send(request);
        return result;
    }

    [HttpPost]
    [Route(PaymentMethodApiEndPointController.UPDATE)]
    public async Task<Response> UpdatePaymentMethod(UpdatePaymentMethodRequest request)
    {
        var result = await _mediator.Send(request);
        return result;
    }

    [HttpDelete]
    [Route(PaymentMethodApiEndPointController.DELETE)]
    [SwaggerRequestType(typeof(DeletePaymentMethodRequest))]
    public async Task<Response> DeletePaymentMethod(string id, bool includeDependencies = true)
    {
        var result = await _mediator.Send(new DeletePaymentMethodRequest(id, includeDependencies));
        return result;
    }
}