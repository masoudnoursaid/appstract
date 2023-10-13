using Appstract.Front.Application.Services;
using Appstract.Front.Domain.Models.ApiResponseModels;
using Appstract.Front.Domain.Models.Profile;
using Appstract.Front.InfrastructureMock.MockData;

namespace Appstract.Front.InfrastructureMock.FakeServices
{
    public class FakeProfileService : IProfileService
    {
        public Task<ApiResponseBase<ProfileInfoResponse>> GetProfileSettingAsync()
        {
            return Task.FromResult(PersonalInfoMockData.GetProfileInfoResponse());
        }
    }
}