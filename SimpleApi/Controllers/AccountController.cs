using Application.Account.Queries.GetInfo;
using ErrorHandling;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SimpleApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ActionResult<Response<GetInfoDto>>> GetInfo([FromQuery] GetInfoRequest request)
    {
        return await _mediator.Send(request);
    }
}