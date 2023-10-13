using Appstract.Front.Application.Common.Constants;
using Appstract.Front.Application.Services;
using Appstract.Front.Domain.Models.ApiResponseModels;
using Microsoft.Extensions.Logging;
using UltraTone.ClientSdk.Customer.Web.V1;
using ErrorDomain = Appstract.Front.Domain.Models.ApiResponseModels.Error;

namespace Appstract.Front.Infrastructure.Services;

public class ErrorMessageService : IErrorMessageService
{
    public ErrorMessageService(
        IErrorMessagesClient errorMessagesClient,
        IPlatformStorageService localStorage,
        ILogger logger)
    {
        _errorMessagesClient = errorMessagesClient;
        _logger = logger;
        _localStorage = localStorage;
    }

    private readonly IErrorMessagesClient _errorMessagesClient;
    private readonly IPlatformStorageService _localStorage;
    private readonly ILogger _logger;

    public async Task<string> GetErrorMessageAsync(string errorCode)
    {
        IDictionary<string, string>? errorMessageList = await GetErrorMessageList();

        if (!string.IsNullOrWhiteSpace(errorCode))
        {
            string[] parts = errorCode.Split('_');
            if (parts.Length == 3 && int.TryParse(parts[2], out int code) && code is >= 600 and <= 999)
            {
                errorCode = "00_000_" + code;
            }
        }

        if (errorMessageList != null && errorMessageList.TryGetValue(errorCode, out string? message))
        {
            return message;
        }

        return errorMessageList?["default_error_message"] ?? string.Empty;
    }

    public async Task<IDictionary<string, string>?> GetErrorMessageList()
    {
        try
        {
            if (await _localStorage.ContainKeyAsync(UltraToneStorage.ERROR_MESSAGES))
            {
                return await _localStorage.GetItemAsync<Dictionary<string, string>>(UltraToneStorage.ERROR_MESSAGES);
            }

            ApiResponseBase<Dictionary<string, string>> response = await GetErrorMessagesListAsync();
            return response.Success ? response.Data : new Dictionary<string, string>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in UltraToneServices.GetErrorMessageList");
            return new Dictionary<string, string>();
        }
    }

    public async Task<ApiResponseBase<Dictionary<string, string>>> GetErrorMessagesListAsync()
    {
        ApiResponseBase<Dictionary<string, string>> response = new();
        GetCustomerErrorMessagesResponse? dictionaryResponse =
            await _errorMessagesClient.ErrorMessagesAsync(FrontendClientType.Web, "en-US");

        if (dictionaryResponse is null)
        {
            return response;
        }

        if (dictionaryResponse.Success)
        {
            response.Success = true;
            response.Data = dictionaryResponse.Data
                .ToDictionary(kv => kv.Key, kv => kv.Value);

            await _localStorage.SetItemAsync(
                UltraToneStorage.ERROR_MESSAGES, response.Data);
        }
        else
        {
            response.Error = new ErrorDomain
            {
                Code = (int)dictionaryResponse.Error.Code, Type = (int)dictionaryResponse.Error.Type
            };
        }

        return response;
    }
}