using Appstract.Front.Domain.Models.ApiResponseModels;
using Appstract.Front.Domain.Models.Profile;

namespace Appstract.Front.Application.Services;

public interface IProfileService
{
    Task<ApiResponseBase<ProfileInfoResponse>> GetProfileSettingAsync();
}