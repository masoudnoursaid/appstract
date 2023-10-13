using Application.Payment.Queries.PaymentMethodList;
using Appstract.Web.Swagger;
using Appstract.WebApi.Models;
using ClientSdk.PaymentApi.V1;
using ErrorHandling;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using CreatePaymentRequest = Application.Payment.Commands.CreatePayment.CreatePaymentRequest;
using PaymentMethodListDto = ClientSdk.PaymentApi.V1.PaymentMethodList;
using PaymentControllerEndPoint = Appstract.WebApi.Common.Consts.EndPoints.PaymentController;

namespace Appstract.WebApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route(PaymentControllerEndPoint.CONTROLLER)]
public class PaymentController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route(PaymentControllerEndPoint.PAYMENT_METHOD_LIST)]
    [SwaggerRequestType(typeof(GetPaymentMethodListRequest))]
    public async Task<ActionResult<Response<PaymentMethodListDto>>> GetPaymentMethodList()
    {
        Response<PaymentMethodListDto> result = await _mediator.Send(new GetPaymentMethodListRequest());
        return result;
    }

    [HttpPost]
    [Route(PaymentControllerEndPoint.PAYMENT_METHOD_LIST)]
    public async Task<ActionResult<Response<CreatePaymentDto>>> CreatePayment(CreatePaymentRequest request)
    {
        Response<CreatePaymentDto> result = await _mediator.Send(request);
        return result;
    }

    [HttpPost]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("unique-path-to-payment-hook")]
    public Task PayHubWebHook(WebHookNotifyModel model)
    {
        return Task.CompletedTask;
    }
}