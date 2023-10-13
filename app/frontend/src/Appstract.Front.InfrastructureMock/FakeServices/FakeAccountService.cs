using Appstract.Front.Application.Services.BackendApis;
using Appstract.Front.Domain.Models.Account;
using Appstract.Front.Domain.Models.ApiResponseModels;
using Appstract.Front.Domain.Models.Profile;
using Appstract.Front.InfrastructureMock.MockData;

namespace Appstract.Front.InfrastructureMock.FakeServices
{
    public class FakeAccountService : IAccountService
    {
        public Task<ApiResponseBase<ProfileInfoResponse>> GetProfileSettingAsync()
        {
            return Task.FromResult(PersonalInfoMockData.GetProfileInfoResponse());
        }

        public Task<ApiResponseBase<AccountInfoResponse>> GetInfoAsync(int? birthYear, CancellationToken cancellationToken)
        {
            return Task.FromResult(new ApiResponseBase<AccountInfoResponse>
            {
                Success = birthYear.HasValue,
                Data = birthYear.HasValue
                    ? new AccountInfoResponse { Age = CalculateAge(birthYear.Value) }
                    : new AccountInfoResponse { Age = 0 }
            });
        }

        private static int CalculateAge(int birthYear)
        {
            var span = DateTime.UtcNow - new DateTime(birthYear, 0, 0, 0, 0, 0, DateTimeKind.Utc);

            var zeroDate = new DateTime(0, 0, 0, 0, 0, 0, DateTimeKind.Utc);
            var age1 = (zeroDate + span).Year;
            return age1;
        }
    }
}