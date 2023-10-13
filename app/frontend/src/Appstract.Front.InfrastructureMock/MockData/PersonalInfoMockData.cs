using Appstract.Front.Domain.Models.ApiResponseModels;
using Appstract.Front.Domain.Models.Profile;

namespace Appstract.Front.InfrastructureMock.MockData
{
    public static class PersonalInfoMockData
    {
        public static ApiResponseBase<ProfileInfoResponse> GetProfileInfoResponse()
        {
            return new ApiResponseBase<ProfileInfoResponse>
            {
                Success = true,
                Data = new ProfileInfoResponse
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john@doe.com",
                    Phone = "+60121234567",
                    RecoveryEmail = "john.doe@gmail.com",
                    UserImage = string.Empty
                }
            };
        }
    }
}