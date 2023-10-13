using Application.Business.Payment.Commands.CancelPayment;
using Application.Business.Payment.Commands.VerifyPayment;
using Global.Mvc.Common.Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using GlobalPaymentMvcEndPoint = Infrastructure.Common.Consts.EndPoints.GlobalPaymentMvc;

namespace Global.Mvc.Controllers;

[Controller]
[Route(GlobalPaymentMvcEndPoint.STRIPE_CONTROLLER)]
public class StripeController : Controller
{
    private readonly IMediator _mediator;

    public StripeController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route(GlobalPaymentMvcEndPoint.VERIFY)]
    public async Task<IActionResult> Verify(string paymentId, string providedId = null!)
    {
        _ = await _mediator.Send(new VerifyPaymentRequest(providedId, paymentId));
        return VisualizeControllerUtils.VisualizeResult(paymentId);
    }

    [HttpGet]
    [Route(GlobalPaymentMvcEndPoint.CANCEL)]
    public async Task<IActionResult> Cancel(string id)
    {
        _ = await _mediator.Send(new CancelPaymentRequest(id));
        return VisualizeControllerUtils.VisualizeCancel(id);
    }
}