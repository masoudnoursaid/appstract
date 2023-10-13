using Application.Business.Payment.Commands.CancelPayment;
using Application.Business.Payment.Commands.VerifyPayment;
using Global.Mvc.Common.Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using GlobalPaymentMvcEndPoint = Infrastructure.Common.Consts.EndPoints.GlobalPaymentMvc;

namespace Global.Mvc.Controllers;

[Controller]
[Route(GlobalPaymentMvcEndPoint.MOLLIE_CONTROLLER)]
public class MollieController : Controller
{
    private readonly IMediator _mediator;

    public MollieController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPost]
    [Route(GlobalPaymentMvcEndPoint.CALLBACK_VERIFY)]
    public Task<IActionResult> Verify()
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [Route(GlobalPaymentMvcEndPoint.REDIRECT_VERIFY)]
    public async Task<IActionResult> Verify(string paymentId, string providedId = null!)
    {
        _ = await _mediator.Send(new VerifyPaymentRequest(providedId, paymentId));
        return VisualizeControllerUtils.VisualizeResult(paymentId);
    }


    [HttpGet]
    [Route(GlobalPaymentMvcEndPoint.CANCEL)]
    public async Task<IActionResult> Cancel(string paymentId)
    {
        _ = await _mediator.Send(new CancelPaymentRequest(paymentId));
        return VisualizeControllerUtils.VisualizeCancel(paymentId);
    }
}