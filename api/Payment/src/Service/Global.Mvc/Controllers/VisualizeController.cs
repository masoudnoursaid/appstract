using Application.Business.Payment.Queries.PaymentVisualize;
using ErrorHandling;
using Infrastructure.Services.PaymentWebHookHttpClient;
using Infrastructure.Services.PaymentWebHookHttpClient.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Common.Swagger;
using VisualizeControllerEndPoint = Global.Mvc.Common.Constants.EndPoints.VisualizeController;

namespace Global.Mvc.Controllers;

[Controller]
[Route(VisualizeControllerEndPoint.CONTROLLER)]
public class VisualizeController : Controller
{
    private readonly IMediator _mediator;
    private readonly IPaymentWebHookHttpClient _hookHttpClient;

    public VisualizeController(IMediator mediator, IPaymentWebHookHttpClient hookHttpClient)
    {
        _mediator = mediator;
        _hookHttpClient = hookHttpClient;
    }


    [HttpGet]
    [Route(VisualizeControllerEndPoint.PAYMENT_RESULT)]
    [SwaggerRequestType(typeof(PaymentVisualizeRequest))]
    public async Task<IActionResult> PaymentResult(string paymentId)
    {
        Response<PaymentVisualizeDto> result = await _mediator.Send(new PaymentVisualizeRequest(paymentId));

        await _hookHttpClient.SendResultToClient(new NotifyClientForPaymentResultDto(
            result.Data?.PaymentId!,
            result.Data?.ProvidedId!,
            result.Data?.Transaction?.Successful ?? false,
            result.Data?.StatusTitle!,
            result.Data?.Amount!,
            result.Data?.Transaction?.InvoiceNumber!), result.Data?.WebHookUrl!, result.Data?.ApplicationSecret!);

        return View("Result", result.Data);
    }

    [HttpGet]
    [Route(VisualizeControllerEndPoint.PAYMENT_CANCEL)]
    [SwaggerRequestType(typeof(PaymentVisualizeRequest))]
    public async Task<IActionResult> PaymentCancel(string paymentId)
    {
        Response<PaymentVisualizeDto> result = await _mediator.Send(new PaymentVisualizeRequest(paymentId));

        await _hookHttpClient.SendResultToClient(new NotifyClientForPaymentResultDto(
            result.Data?.PaymentId!,
            result.Data?.ProvidedId!,
            result.Data?.Transaction?.Successful ?? false,
            result.Data?.StatusTitle!,
            result.Data?.Amount!,
            result.Data?.Transaction?.InvoiceNumber!), result.Data?.WebHookUrl!, result.Data?.ApplicationSecret!);

        return View("Cancel", result.Data);
    }
}