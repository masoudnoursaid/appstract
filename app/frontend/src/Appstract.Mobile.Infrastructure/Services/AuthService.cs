using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using Appstract.Mobile.Application.Common.Consts;
using Appstract.Mobile.Application.Common.Models;
using Appstract.Mobile.Application.Dto.Authentication;
using Appstract.Mobile.Application.Services;
using UltraTone.ClientSdk.Customer.Mobile.V1;

namespace Appstract.Mobile.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly IAppInfo _appInfo;
    private readonly IAppleSignInAuthenticator _appleSignInAuthenticator;
    private readonly IAuthClient _authClient;
    private readonly IDeviceDisplay _deviceDisplay;
    private readonly IDeviceInfo _deviceInfo;

    private readonly bool _isVirtual = DeviceInfo.Current.DeviceType switch
    {
        DeviceType.Physical => false,
        DeviceType.Virtual => true,
        _ => false
    };

    private readonly IWebAuthenticator _webAuthenticator;

    public AuthService(IAuthClient authClient, IDeviceInfo deviceInfo,
        IAppInfo appInfo, IDeviceDisplay deviceDisplay, IWebAuthenticator webAuthenticator,
        IAppleSignInAuthenticator appleSignInAuthenticator)
    {
        _authClient = authClient;
        _deviceInfo = deviceInfo;
        _appInfo = appInfo;
        _deviceDisplay = deviceDisplay;
        _webAuthenticator = webAuthenticator;
        _appleSignInAuthenticator = appleSignInAuthenticator;
    }

    public async void AddToken(string token, string refresh)
    {
        await SecureStorage.Default.SetAsync(SecureStorageLocal.ACCESS_TOKEN, token);
        await SecureStorage.Default.SetAsync(SecureStorageLocal.REFRESH_TOKEN, refresh);
        AddTokenTime(token);
    }

    public async void AddTokenTime(string token)
    {
        JwtSecurityToken jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
        TimeSpan validTime = jwt.ValidTo - jwt.ValidFrom;
        DateTime time = DateTime.Now.AddHours(validTime.Hours).AddMinutes(validTime.Minutes).AddMinutes(-1);
        await SecureStorage.Default.SetAsync(SecureStorageLocal.TIME_TOKEN, time.ToString(CultureInfo.CurrentCulture));
    }

    public void DeleteToken()
    {
        SecureStorage.Default.Remove(SecureStorageLocal.ACCESS_TOKEN);
        SecureStorage.Default.Remove(SecureStorageLocal.REFRESH_TOKEN);
    }

    public async Task<ApiResult<EmailOtpResultDto>> EmailOtpAsync(string email)
    {
        try
        {
            RequestEmailOtpResultDtoResponse result =
                await _authClient.RequestEmailOtpAsync(new RequestEmailOtpRequest { Email = email });

            EmailOtpResultDto emailOtpResult = new()
            {
                RequestsCount = result.Data?.RequestsCount ?? 0,
                CreatedAt = result.Data?.CreatedAt,
                Ttl = result.Data?.Ttl ?? 0,
                RateLimit = new Application.Common.Models.RateLimit
                {
                    Count = result.Data?.RateLimit?.Count ?? 0,
                    MinimumInterval = result.Data?.RateLimit?.MinimumInterval ?? 0,
                    Period = result.Data?.RateLimit?.Period ?? 0
                }
            };

            return new ApiResult<EmailOtpResultDto>()
            {
                Data = emailOtpResult,
                Success = result.Success,
                Error = new ApiError
                {
                    Code = (int?)result.Error?.Code,
                    Type = (int?)result.Error?.Type,
                    Values = result.Error?.Values
                }
            };
        }
        catch (Exception)
        {
            return new ApiResult<EmailOtpResultDto>() { Success = false };
        }
    }

    public async Task<string> ExternalEmailAsync(string scheme)
    {
        string authToken = string.Empty;

        try
        {
            WebAuthenticatorResult result;

            if (scheme.Equals("Apple")
                && DeviceInfo.Platform == DevicePlatform.iOS
                && DeviceInfo.Version.Major >= 13)
            {
                // Use Native Apple Sign In API's
                result = await AppleSignInAuthenticator.AuthenticateAsync();
            }
            else
            {
                // Web Authentication flow
                Uri authUrl = new Uri($"ultratone://");
                Uri callbackUrl = new("ultratone://");

                result = await WebAuthenticator.Default.AuthenticateAsync(authUrl, callbackUrl);
            }

            if (result.Properties.TryGetValue("name", out string name) && !string.IsNullOrEmpty(name))
            {
                authToken += $"Name: {name}{Environment.NewLine}";
            }

            if (result.Properties.TryGetValue("email", out string email) && !string.IsNullOrEmpty(email))
            {
                authToken += $"Email: {email}{Environment.NewLine}";
            }

            // Note that Apple Sign In has an IdToken and not an AccessToken
            authToken += result?.AccessToken ?? result?.IdToken;
        }
        catch (TaskCanceledException)
        {
        }

        return authToken;
    }

    public async Task<RefreshTokenModel> GetToken()
    {
        string token = await SecureStorage.Default.GetAsync(SecureStorageLocal.ACCESS_TOKEN);
        string refresh = await SecureStorage.Default.GetAsync(SecureStorageLocal.REFRESH_TOKEN);

        return new RefreshTokenModel { AccessToken = token, RefreshToken = refresh };
    }

    public async Task<ApiResult<PhoneOtpDto>> PhoneOtpAsync(string phone)
    {
        try
        {
            RequestPhoneOtpResultDtoResponse result =
                await _authClient.RequestPhoneOtpAsync(new RequestPhoneOtpRequest { PhoneNumber = phone });

            PhoneOtpDto phoneOtpResult = new()
            {
                RequestsCount = result.Data?.RequestsCount ?? 0,
                CreatedAt = result.Data?.CreatedAt,
                Ttl = result.Data?.Ttl ?? 0,
                RateLimit = new Application.Common.Models.RateLimit
                {
                    Count = result.Data?.RateLimit?.Count ?? 0,
                    MinimumInterval = result.Data?.RateLimit?.MinimumInterval ?? 0,
                    Period = result.Data?.RateLimit?.Period ?? 0
                }
            };

            return new ApiResult<PhoneOtpDto>()
            {
                Data = phoneOtpResult,
                Success = result.Success,
                Error = new ApiError
                {
                    Code = (int?)result.Error?.Code,
                    Type = (int?)result.Error?.Type,
                    Values = result.Error?.Values
                }
            };
        }
        catch (Exception)
        {
            return new ApiResult<PhoneOtpDto>() { Success = false };
        }
    }

    public async Task<bool> RegisterDeviceAsync()
    {
        RegisterDeviceResultDtoResponse response = await _authClient.RegisterDeviceAsync(new RegisterDeviceRequest
        {
            AppVersion = _appInfo.VersionString,
            BuildNumber = _appInfo.BuildString,
            Os = _deviceInfo.Platform.ToString(),
            OsVersion = _deviceInfo.VersionString,
            Manufacturer = _deviceInfo.Manufacturer,
            Model = _deviceInfo.Model,
            IsTablet = _deviceInfo.Idiom == DeviceIdiom.Tablet,
            IsEmulator = _isVirtual,
            DisplayHeight = Convert.ToInt32(_deviceDisplay.MainDisplayInfo.Height),
            DisplayWidth = Convert.ToInt32(_deviceDisplay.MainDisplayInfo.Width),
            DisplayOrientation = _deviceDisplay.MainDisplayInfo.Orientation.ToString()
        });

        if (!response.Success)
        {
            return false;
        }

        AddToken(response.Data?.AccessToken, response.Data?.RefreshToken);
        return true;
    }

    public async void RenewToken()
    {
        RefreshTokenModel token = await GetToken();
        RefreshTokenResultDtoResponse response = await _authClient.RefreshTokenAsync(new RefreshTokenRequest
        {
            AccessToken = token.AccessToken,
            RefreshToken = token.RefreshToken
        });

        if (response.Success)
        {
            AddToken(response.Data?.AccessToken, response.Data?.RefreshToken);
        }
    }

    public async void ValidTokenTime()
    {
        string validTime = await SecureStorage.Default.GetAsync(SecureStorageLocal.TIME_TOKEN);
        TimeSpan time = DateTime.Parse(validTime) - DateTime.Now;
        if (time.Minutes < 0)
        {
            RenewToken();
        }
    }

    public async Task<ApiResult<VerifyEmailOtpDto>> VerifyEmailOtpAsync(string code)
    {
        try
        {
            VerifyEmailOtpResultDtoResponse result =
                await _authClient.VerifyEmailOtpAsync(
                    new VerifyEmailOtpRequest { Code = code });

            VerifyEmailOtpDto verifyEmailResult = new()
            {
                NewUser = result.Data?.NewUser,
                AccessToken = result.Data?.AccessToken,
                RefreshToken = result.Data?.RefreshToken
            };

            if (result.Data?.AccessToken is not null && result.Data?.RefreshToken is not null)
            {
                AddToken(result.Data.AccessToken, result.Data.RefreshToken);
            }

            return new ApiResult<VerifyEmailOtpDto>()
            {
                Data = verifyEmailResult,
                Success = result.Success,
                Error = new ApiError
                {
                    Code = (int?)result.Error?.Code,
                    Type = (int?)result.Error?.Type,
                    Values = result.Error?.Values
                }
            };
        }
        catch (Exception)
        {
            return new ApiResult<VerifyEmailOtpDto>() { Success = false };
        }
    }

    public async Task<ApiResult<VerifyPhoneOtpDto>> VerifyPhoneOtpAsync(string code)
    {
        try
        {
            VerifyPhoneOtpResultDtoResponse result =
                await _authClient.VerifyPhoneOtpAsync(
                    new VerifyPhoneOtpRequest { Code = code });

            VerifyPhoneOtpDto verifyPhoneResult = new();

            if (result.Data?.AccessToken is not null && result.Data?.RefreshToken is not null)
            {
                verifyPhoneResult.AccessToken = result.Data?.AccessToken!;
                verifyPhoneResult.RefreshToken = result.Data?.RefreshToken!;
                AddToken(result.Data?.AccessToken, result.Data?.RefreshToken);
            }

            return new ApiResult<VerifyPhoneOtpDto>()
            {
                Data = verifyPhoneResult,
                Success = result.Success,
                Error = new ApiError
                {
                    Code = (int?)result.Error?.Code,
                    Type = (int?)result.Error?.Type,
                    Values = result.Error?.Values
                }
            };
        }
        catch (Exception)
        {
            return new ApiResult<VerifyPhoneOtpDto>() { Success = false };
        }
    }
}