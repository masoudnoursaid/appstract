using Application.Business.Payment.Commands.CancelPayment;
using Application.Business.Payment.Commands.VerifyPayment;
using Global.Mvc.Common.Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using GlobalPaymentMvcEndPoint = Infrastructure.Common.Consts.EndPoints.GlobalPaymentMvc;

namespace Global.Mvc.Controllers;

[Controller]
[Route(GlobalPaymentMvcEndPoint.PAYPAL_CONTROLLER)]
public class PaypalController : Controller
{
    private readonly IMediator _mediator;

    public PaypalController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route(GlobalPaymentMvcEndPoint.VERIFY)]
    public async Task<IActionResult> Verify(string id, string paymentId = null!)
    {
        _ = await _mediator.Send(new VerifyPaymentRequest(paymentId, id));
        return VisualizeControllerUtils.VisualizeResult(id);
    }

    [HttpGet]
    [Route(GlobalPaymentMvcEndPoint.CANCEL)]
    public async Task<IActionResult> Cancel(string id)
    {
        _ = await _mediator.Send(new CancelPaymentRequest(id));
        return VisualizeControllerUtils.VisualizeCancel(id);
    }
}