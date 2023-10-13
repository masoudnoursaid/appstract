using Appstract.Front.Application.Common.Constants;
using Appstract.Front.Application.Common.Utilities;
using Blazored.LocalStorage;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace Appstract.Front.Infrastructure.Services;

public class BaseInfoServices
{
    private readonly ISyncLocalStorageService _localStorage;
    private readonly IJSRuntime _jsRuntime;
    private readonly ILogger _logger;

    public BaseInfoServices(ISyncLocalStorageService localStorageService, IJSRuntime jsRuntime, ILogger logger)
    {
        _localStorage = localStorageService;
        _jsRuntime = jsRuntime;
        _logger = logger;
    }

    public string UserTimeZone =>
        _userTimeZone ??= _localStorage.GetItemAsString(UltraToneStorage.USER_TIME_ZONE) ?? string.Empty;

    private string? _userTimeZone;

    public async Task SetUserTimeZone()
    {
        try
        {
            _userTimeZone =
                await _jsRuntime.InvokeAsync<string>("eval", "Intl.DateTimeFormat().resolvedOptions().timeZone");
            _localStorage.SetItemAsString(UltraToneStorage.USER_TIME_ZONE, _userTimeZone);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in BaseInfoServices.SetUserTimeZone");
        }
    }

    public string BaseCurrency =>
        _baseCurrency ??= _localStorage.GetItemAsString(UltraToneStorage.BASE_CURRENCY) ?? string.Empty;

    private string? _baseCurrency;

    public void SetBaseCurrency(string baseCurrency)
    {
        _localStorage.SetItemAsString(UltraToneStorage.BASE_CURRENCY, baseCurrency);
    }

    public string UserCulture =>
        _userCulture ??= _localStorage.GetItemAsString(UltraToneStorage.USER_CULTURE) ?? "en-US";

    private string? _userCulture;

    public void SetUserCultureByPhoneNumber(string phoneNumber)
    {
        string cultureName = "en-US";
        if (!string.IsNullOrWhiteSpace(phoneNumber))
        {
            string countryCode = PhoneUtil.GetCountryCode(phoneNumber);
            cultureName = CultureUtil.GetByCountryCode(countryCode)?.Name ?? string.Empty;
        }

        _localStorage.SetItemAsString(UltraToneStorage.USER_CULTURE, cultureName);
    }
}