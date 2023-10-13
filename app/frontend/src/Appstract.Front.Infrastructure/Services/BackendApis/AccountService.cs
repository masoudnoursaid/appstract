using Appstract.Front.Application.Services;
using Appstract.Front.Application.Services.BackendApis;
using Appstract.Front.Domain.Models.Account;
using Appstract.Front.Domain.Models.ApiResponseModels;
using ClientSdk.Customer.Web.V1;
using Error = Appstract.Front.Domain.Models.ApiResponseModels.Error;


namespace Appstract.Front.Infrastructure.Services.BackendApis;

public class AccountService : IAccountService
{
    private readonly IAccountClient _accountClient;
    private readonly IErrorMessageService _errorMessageService;

    public AccountService(IAccountClient accountClient, IErrorMessageService errorMessageService)
    {
        _accountClient = accountClient;
        _errorMessageService = errorMessageService;
    }

    public async Task<ApiResponseBase<AccountInfoResponse>> GetInfoAsync(int? birthYear, CancellationToken cancellationToken = default)
    {
        ApiResponseBase<AccountInfoResponse> response = new();
        GetInfoResponse accountInfo = await _accountClient.AccountAsync(birthYear, cancellationToken);

        if (accountInfo is null)
        {
            return response;
        }

        if (accountInfo.Success)
        {
            response.Success = true;
            response.Data = new AccountInfoResponse
            {
                Age = accountInfo.Data.Age
            };
        }
        else
        {
            response.Error = new Error { Code = (int)accountInfo.Error.Code, Type = (int)accountInfo.Error.Type };
            response.Error.Message = await _errorMessageService.GetErrorMessageAsync(response.Error.FormattedCode);
        }

        return response;
    }
}