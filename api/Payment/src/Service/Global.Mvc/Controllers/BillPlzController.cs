using Application.Business.Payment.Commands.CancelPayment;
using Application.Business.Payment.Commands.VerifyPayment;
using Global.Mvc.Common.Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using GlobalPaymentMvcEndPoint = Infrastructure.Common.Consts.EndPoints.GlobalPaymentMvc;

namespace Global.Mvc.Controllers;

[Controller]
[Route(GlobalPaymentMvcEndPoint.BILLPLZ_CONTROLLER)]
public class BillPlzController : Controller
{
    private readonly IMediator _mediator;

    public BillPlzController(IMediator mediator)
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
    public async Task<IActionResult> Verify(string paymentId, [FromQuery(Name = "Billplz[id]")] string id = "NAN")
    {
        _ = await _mediator.Send(new VerifyPaymentRequest(id, paymentId));
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