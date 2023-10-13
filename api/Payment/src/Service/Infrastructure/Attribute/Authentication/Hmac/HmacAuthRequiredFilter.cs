using System.Security.Authentication;
using Application.Common.Consts.Authentication;
using Application.Repositories.Generic.Read;
using Application.Services.IP;
using Domain.Common.Hmac;
using Domain.Common.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using ApplicationEntity = Domain.Entities.Application;

namespace Infrastructure.Attribute.Authentication.Hmac;

public class HmacAuthRequiredFilter : IAuthorizationFilter
{
    private readonly IReadRepository<ApplicationEntity> _applicationReadRepository;
    private readonly IIpService _ipService;
    private readonly ILogger<HmacAuthRequiredFilter> _logger;

    public HmacAuthRequiredFilter([FromServices] IReadRepository<ApplicationEntity> applicationReadRepository,
        ILogger<HmacAuthRequiredFilter> logger, IIpService ipService)
    {
        _applicationReadRepository = applicationReadRepository;
        _logger = logger;
        _ipService = ipService;
    }

    public async void OnAuthorization(AuthorizationFilterContext context)
    {
        var request = context.HttpContext.Request;
        var signature = request.Headers[HmacAuthentication.SIGNATURE_HEADER].ToString();
        var date = request.Headers[HmacAuthentication.DATE_HEADER].ToString();
        var nonce = request.Headers[HmacAuthentication.NONCE_HEADER].ToString();
        var apiKey = request.Headers[HmacAuthentication.API_KEY_HEADER].ToString();
        var app = await _applicationReadRepository
            .Get(c => c.ApiKey!.Value == apiKey, exceptionRaiseIfNotExist: false);

        if (app == null)
            throw new AuthenticationException($"Invalid Api Key : {apiKey}");
        var secret = app.ApiSecret!.Value;


        var info =
            new HmacInfoObject(new Uri($"{request.Scheme}://{request.Host}"), new HttpMethod(request.Method),
                long.Parse(date), nonce);
        var hmacHash = await HmacUtils.ComputeHmacSha256(info, secret);

        if (hmacHash != signature)
            throw new AuthenticationException($"Invalid Signature {signature}");

        _logger.LogInformation("HMAC_AUTH_ATTRIBUTE.ON_AUTH --- App {appId} passed --- ip : {ip}", app.Title,
            _ipService.GetRawRemoteIpAddress());
    }
}