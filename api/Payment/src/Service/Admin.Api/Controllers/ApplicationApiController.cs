using Application.Business.Applications.Command.RegisterApplication;
using Application.Business.Applications.Queries.ApplicationList;
using Application.Common.Consts;
using ErrorHandling;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Common.Swagger;
using ApplicationApiEndPoint = Admin.Api.Constants.EndPoints.ApplicationApi;

namespace Admin.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route(ApplicationApiEndPoint.CONTROLLER)]
public class ApplicationApiController : ControllerBase
{
    private readonly IMediator _mediator;

    public ApplicationApiController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route(ApplicationApiEndPoint.LIST)]
    [SwaggerRequestType(typeof(ApplicationListRequest))]
    public async Task<ActionResult<Response<ApplicationsList>>> GetAllApplications(int? page = null,
        int? perPage = null)
    {
        var result = await _mediator.Send(new ApplicationListRequest(perPage ?? Pagination.PER_PAGE, page));
        return result;
    }


    [HttpPut]
    [Route(ApplicationApiEndPoint.CREATE)]
    public async Task<ActionResult<Response<RegisterApplicationResultDto>>> RegisterApplication(
        RegisterApplicationRequest request)
    {
        var result = await _mediator.Send(request);
        return result;
    }
}