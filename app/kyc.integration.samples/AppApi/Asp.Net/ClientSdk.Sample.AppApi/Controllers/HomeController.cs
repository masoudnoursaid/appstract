using App.SDK.Common.Enum;
using App.SDK.Common.Model;
using App.SDK.Service.KycClient;
using ClientSdk.AppApi.V1;
using ClientSdk.Sample.AppApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClientSdk.Sample.AppApi.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IKycClient _kycClient;

    public HomeController(ILogger<HomeController> logger, IKycClient kycClient)
    {
        _logger = logger;
        _kycClient = kycClient;
    }

    public async Task<IActionResult> Index()
    {
        // Always create unique client id for your each clients
        string clientId = Guid.NewGuid().ToString();

        // Generate session with client id
        Result<CreateNewSessionResultDto> result = await _kycClient.GenerateSession(clientId);

        if (result.Success)
        {
            DateTime expiredDate = DateTime.UtcNow.AddDays(result.Data!.ExpireTimeInDays);
            string token = result.Data.Token;
            _logger.LogInformation("Session generate successfully --- {token}", token);

            // Add to storage
            LocalStorage.Add(new KycModel(token, clientId, DateOnly.FromDateTime(expiredDate)));

            return Ok(result.Data);
        }
        else
        {
            AppApiClientErrorCodes code = result.Error!.Code;
            Dictionary<string, string> values = result.Error.Values!;
            _logger.LogInformation("Session generation failed  --- {code} --- {values}", code,
                string.Join(',', values));
            return Ok(result.Error);
        }
    }


    [Route("My_Webhook")]
    public async Task<IActionResult> WebHook(string token, KycProcessStatus status)
    {
        // First validate token
        KycModel model = await LocalStorage.GetByToken(token);
        model.status = status;

        // Then check the status
        switch (status)
        {
            case KycProcessStatus.Fail:
                // Sorry! but your client did not successfully pass the KYC process.
                // Please create another session for your client
                break;
            case KycProcessStatus.Timeout:
                // Sorry! The token has expired, kindly create a new session for your client.
                break;
            case KycProcessStatus.Invalid:
                // Oops! We are experiencing server issues. Please wait for another callback.
                break;
            case KycProcessStatus.Verified:
                // Congratulations! Your client has been successfully approved!
                break;
        }

        return Ok();
    }
}