using System.Data.Common;
using Application.Common.Extensions;
using Application.Common.Validators;
using Application.Services.IP;
using ErrorHandling;
using ErrorHandling.Enums;
using ErrorHandling.Helpers;
using ErrorHandling.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Common.PipelineBehaviors;

public class CommonErrorsPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : struct, IResponse
{
    private readonly IIpService _ipService;
    private readonly ILogger<CommonErrorsPipelineBehavior<TRequest, TResponse>> _logger;
    private readonly IRequestValidator<TRequest> _requestValidator;

    public CommonErrorsPipelineBehavior(
        ILogger<CommonErrorsPipelineBehavior<TRequest, TResponse>> logger,
        IRequestValidator<TRequest> requestValidator,
        IIpService ipService)
    {
        _logger = logger;
        _requestValidator = requestValidator;
        _ipService = ipService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var handlerCode = request.GetHandlerCode();
        var handlerNumber = ErrorCodeHelper.Format((int)handlerCode);

        LogRequestStarting(handlerCode, handlerNumber);
        _logger.LogDebug("HANDLER.REQUEST.DETAILS --- {HandlerCode} ({HandlerName}) --- {Request}"
            , handlerNumber, handlerCode, request);

        var errorList = await _requestValidator.ValidateAsync(request, cancellationToken);

        TResponse response;

        if (errorList.Any())
        {
            response = new TResponse
            {
                Success = false,
                Error = CommonErrorCode.ValidationFailed.ForRequest(
                    request,
                    errorList.GetRange(0, 1).ToDictionary(x => x.PropertyName
                        , x => x.ErrorMessage))
            };
            _logger.LogWarning(
                "HANDLER.ERROR.VALIDATION --- {HandlerCode} ({HandlerName}) --- ErrorCode: {ErrorCode}\n{@Errors}",
                handlerNumber,
                handlerCode,
                ErrorCodeHelper.Format(response.Error.Value.Code),
                errorList);

            return response;
        }

        try
        {
            response = await next();
        }
        catch (Exception e) when (e is ICodedException codedException)
        {
            response = new TResponse
            {
                Success = false,
                Error = codedException.GetCommonErrorCode().ForRequest(request)
            };
            LogError("HANDLER.ERROR.EXPECTED", e, handlerCode, handlerNumber, response.Error.Value.Code);
        }
        catch (Exception e) when (e.InnerException is DbException)
        {
            response = new TResponse
            {
                Success = false,
                Error = CommonErrorCode.DatabaseError.ForRequest(request)
            };
            LogError("HANDLER.ERROR.DATABASE", e, handlerCode, handlerNumber, response.Error.Value.Code);
        }
        catch (Exception e)
        {
            response = new TResponse
            {
                Success = false,
                Error = CommonErrorCode.UnexpectedError.ForRequest(request)
            };
            LogError("HANDLER.ERROR.UNEXPECTED", e, handlerCode, handlerNumber, response.Error.Value.Code);
        }

        if (response.Success)
            _logger.LogInformation(
                "HANDLER.RESPONSE.SUCCESS --- {HandlerCode} ({HandlerName})",
                handlerNumber,
                handlerCode);
        else
            _logger.LogWarning(
                "HANDLER.RESPONSE.FAILURE --- {HandlerCode} ({HandlerName}) --- ErrorCode: {ErrorCode}",
                handlerNumber,
                handlerCode,
                ErrorCodeHelper.Format(response.Error!.Value.Code));

        return response;
    }

    private void LogError(string name, Exception e, HandlerCode handlerCode, string handlerNumber, int errorCode)
    {
        _logger.LogError(
            e,
            "{Name} --- {HandlerCode} ({HandlerName}) --- ErrorCode: {ErrorCode}",
            name,
            handlerNumber,
            handlerCode,
            ErrorCodeHelper.Format(errorCode));
    }

    private void LogRequestStarting(HandlerCode handlerCode, string handlerNumber)
    {
        var ipAddress = _ipService.GetRawRemoteIpAddress() ?? "Unknown IP";
        string? sessionId = null;
        string? cardId = null;
        string? gpn = null;

        _logger.LogInformation(
            "HANDLER.REQUEST.STARTING --- {HandlerCode} ({HandlerName}) --- IP: {Ip} --- Card {{ ID: {CardId} , GPN: {Gpn} }} --- Session ID: {SessionId}",
            handlerNumber,
            handlerCode,
            ipAddress,
            cardId,
            gpn,
            sessionId);
    }
}