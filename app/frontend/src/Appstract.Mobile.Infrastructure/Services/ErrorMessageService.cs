using System.Text.Json;
using Appstract.Front.Application.Common.Constants;
using Appstract.Front.Application.Services;
using Appstract.Front.Domain.Models.ApiResponseModels;
using Microsoft.Extensions.Logging;
using UltraTone.ClientSdk.Customer.Web.V1;
using ErrorDomain = Appstract.Front.Domain.Models.ApiResponseModels.Error;
using ILogger = Appstract.Mobile.Application.Services.ILogger;

namespace Appstract.Front.Mobile.Infrastructure.Services;

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
        IDictionary<string, string> errorMessageList = await GetErrorMessageList();

        if (!string.IsNullOrWhiteSpace(errorCode))
        {
            string[] parts = errorCode.Split('_');
            if (parts.Length == 3)
            {
                if (int.TryParse(parts[2], out int code))
                {
                    if (code is >= 600 and <= 999)
                    {
                        errorCode = "00_000_" + code;
                    }
                }
            }
        }

        if (errorMessageList != null && errorMessageList.TryGetValue(errorCode!, out string message))
        {
            return message;
        }

        return errorMessageList?["default_error_message"] ?? string.Empty;
    }

    public async Task<IDictionary<string, string>> GetErrorMessageList()
    {
        try
        {
            if (await _localStorage.ContainKeyAsync(UltraToneStorage.ERROR_MESSAGES))
            {
                string messageString = await _localStorage.GetItemAsStringAsync(UltraToneStorage.ERROR_MESSAGES);
                Dictionary<string, string> messageDictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(messageString);
                return messageDictionary;
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
        GetCustomerErrorMessagesResponse dictionaryResponse =
            await _errorMessagesClient.ErrorMessagesAsync(FrontendClientType.Mobile, "en-US");

        if (dictionaryResponse is null)
        {
            return response;
        }

        if (dictionaryResponse.Success)
        {
            response.Success = true;
            response.Data = dictionaryResponse.Data
                .ToDictionary(kv => kv.Key, kv => kv.Value);

            string messegeString = JsonSerializer.Serialize(response.Data);

            await _localStorage.SetItemAsStringAsync(
                UltraToneStorage.ERROR_MESSAGES, messegeString);
        }
        else
        {
            response.Error = new ErrorDomain
            {
                Code = (int)dictionaryResponse.Error.Code,
                Type = (int)dictionaryResponse.Error.Type
            };
        }

        return response;
    }
}
