using Application.Business.Payment.Commands.CreatePayment;
using ErrorHandling;
using Infrastructure.Attribute.Authentication.Hmac;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentApiEndPoint = Payment.Api.Constants.EndPoints.PaymentApi;

namespace Payment.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[HmacAuthentication]
[Route(PaymentApiEndPoint.CONTROLLER)]
public class PaymentApiController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentApiController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut]
    [Route(PaymentApiEndPoint.CREATE)]
    public async Task<Response<CreatePaymentDto>> CreatePayment(CreatePaymentRequest request)
    {
        var result = await _mediator.Send(request);
        return result;
    }
}