using Application.Business.Currency.Commands.CreateCurrency;
using Application.Business.Currency.Commands.DeleteCurrency;
using Application.Business.Currency.Commands.UpdateCurrency;
using Application.Business.Currency.Dto;
using Application.Business.Currency.Queries.GetCurrencyList;
using Application.Common.Consts;
using ErrorHandling;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Common.Swagger;
using CurrencyApiEndPointController = Admin.Api.Constants.EndPoints.CurrencyApi;

namespace Admin.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route(CurrencyApiEndPointController.CONTROLLER)]
public class CurrencyApiController : ControllerBase
{
    private readonly IMediator _mediator;

    public CurrencyApiController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route(CurrencyApiEndPointController.LIST)]
    [SwaggerRequestType(typeof(GetCurrencyListRequest))]
    public async Task<Response<CurrencyList>> GetCurrencyList(int? page, int? perPage)
    {
        var result = await _mediator.Send(new GetCurrencyListRequest(perPage ?? Pagination.PER_PAGE, page));
        return result;
    }

    [HttpGet]
    [Route(CurrencyApiEndPointController.GET)]
    public Task<Response<CurrencyDto>> GetCurrency(string id)
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    [Route(CurrencyApiEndPointController.CREATE)]
    public async Task<Response> CreateCurrency(CreateCurrencyRequest request)
    {
        var result = await _mediator.Send(request);
        return result;
    }

    [HttpPost]
    [Route(CurrencyApiEndPointController.UPDATE)]
    public async Task<Response> UpdateCurrency(UpdateCurrencyRequest request)
    {
        var result = await _mediator.Send(request);
        return result;
    }

    [HttpDelete]
    [Route(CurrencyApiEndPointController.DELETE)]
    [SwaggerRequestType(typeof(DeleteCurrencyRequest))]
    public async Task<Response> DeleteCurrency(string id, bool includeDependencies = true)
    {
        var result = await _mediator.Send(new DeleteCurrencyRequest(id, includeDependencies));
        return result;
    }
}