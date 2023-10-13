using Appstract.Front.Application.Services;
using Appstract.Front.Domain.Models.ApiResponseModels;
using Appstract.Front.Domain.Models.Profile;
using UltraTone.ClientSdk.Customer.Web.V1;
using Error = Appstract.Front.Domain.Models.ApiResponseModels.Error;

namespace Appstract.Front.Infrastructure.Services;

public class ProfileService : IProfileService
{
    private readonly IProfileClient _profileClient;
    private readonly IErrorMessageService _errorMessageService;

    public ProfileService(
        IProfileClient profileClient,
        IErrorMessageService errorMessageService)
    {
        _errorMessageService = errorMessageService;
        _profileClient = profileClient;
    }

    public async Task<ApiResponseBase<ProfileInfoResponse>> GetProfileSettingAsync()
    {
        ApiResponseBase<ProfileInfoResponse> response = new();
        GetProfileResponse? profileResponse = await _profileClient.ProfileAsync();

        if (profileResponse is null)
        {
            return response;
        }

        if (profileResponse.Success)
        {
            response.Success = true;
            response.Data = new ProfileInfoResponse
            {
                FirstName = profileResponse.Data.FirstName,
                LastName = profileResponse.Data.LastName,
                Email = profileResponse.Data.Email,
                Phone = profileResponse.Data.Phone,
                Address = profileResponse.Data.Address,
                ZipCode = profileResponse.Data.ZipCode,
                City = profileResponse.Data.City,
                Country = profileResponse.Data.Country,
                State = profileResponse.Data.State,
                Fax = profileResponse.Data.Fax,
            };
        }
        else
        {
            response.Error = new Error { Code = profileResponse.Error.Code, Type = (int)profileResponse.Error.Type };
            response.Error.Message = await _errorMessageService.GetErrorMessageAsync(response.Error.FormattedCode);
        }

        return response;
    }
}