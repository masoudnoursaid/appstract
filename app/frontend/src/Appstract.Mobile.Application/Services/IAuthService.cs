using Appstract.Mobile.Application.Common.Models;
using Appstract.Mobile.Application.Dto.Authentication;

namespace Appstract.Mobile.Application.Services;

public interface IAuthService
{
    void AddTokenTime(string token);
    void ValidTokenTime();
    Task<RefreshTokenModel> GetToken();
    void AddToken(string token, string refresh);
    void DeleteToken();
    void RenewToken();
    Task<string> ExternalEmailAsync(string scheme);
    Task<ApiResult<EmailOtpResultDto>> EmailOtpAsync(string email);
    Task<ApiResult<VerifyEmailOtpDto>> VerifyEmailOtpAsync(string code);
    Task<ApiResult<PhoneOtpDto>> PhoneOtpAsync(string phone);
    Task<ApiResult<VerifyPhoneOtpDto>> VerifyPhoneOtpAsync(string code);
    Task<bool> RegisterDeviceAsync();
}