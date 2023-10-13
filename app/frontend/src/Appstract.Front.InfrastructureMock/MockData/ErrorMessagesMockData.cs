using Appstract.Front.Domain.Models.ApiResponseModels;

namespace Appstract.Front.InfrastructureMock.MockData
{
    public static class ErrorMessagesMockData
    {
        public static Task<ApiResponseBase<Dictionary<string, string>>> GetErrorMessageList()
        {
            return Task.FromResult(new ApiResponseBase<Dictionary<string, string>>
            {
                Data = new Dictionary<string, string>()
                {
                    { "default_error_message", "Internal server error, please try later." },
                    { "11993101", "Given sim card not found." },
                    { "11993102", "Sim card has no inbox." },
                    { "11993103", "Current user credentials not valid." },
                    { "11993600", "An Internal server error occured." },
                    { "11994101", "Given message not found." },
                    { "11994600", "An Internal server error occured." },
                    { "11995101", "System Current User is not valid. " },
                    { "11995102", "Requested sim card can not be found." },
                    { "11995103", "No inbox was found." },
                    { "11995104", "No gsm was found." },
                    { "11995105", "No port was found." },
                    { "11995600", "An Internal server error occured." },
                    { "11997101", "System Current User is not valid. " },
                    { "11997102", "Current user credentials not valid." },
                    { "11997600", "An Internal server error occured." },
                    { "11998101", "System Current User is not valid." },
                    { "11998102", "Requested sim card can not be found." },
                    { "11998103", "User Don't have enough credit." },
                    { "11998104", "Current user credentials not valid." },
                    { "11998600", "An Internal server error occured." },
                    { "11999101", "System Current User is not valid." },
                    { "11999102", "Current user credentials not valid." },
                    { "11999600", "An Internal server error occured." },
                    { "13799101", "Credentials are invalid" },
                    { "13799102", "User account is suspended" },
                    { "13799600", "Failed to sign in due to internal server error, please try again later." },
                    { "13990101", "Something went wrong, do it later." },
                    { "13990102", "Current user credentials not valid." },
                    { "13990600", "An Internal server error occured." },
                    { "13991103", "Current user credentials not valid." },
                    { "13992101", "Please enter valid voucher." },
                    { "13992102", "Please enter valid voucher." },
                    { "13992103", "Please enter valid voucher." },
                    { "13992104", "Please enter valid voucher." },
                    { "13992105", "Please enter valid voucher." },
                    { "13992106", "Please do login again." },
                    { "13992107", "Please try again later." },
                    { "13992108", "Please try again later." },
                    { "13992109", "Current user credentials not valid." },
                    { "13992110", "Account access is denied." },
                    { "13992600", "An Internal server error occured." },
                    { "13996101", "System Current User is not valid." },
                    { "13996102", "Current user credentials not valid." },
                    { "13996600", "An Internal server error occured." },
                    { "13999101", "System Current User is not valid." },
                    { "13999102", "Pagination value is not valid." },
                    { "15998101", "Destination Number is not valid." },
                    { "15998102", "Currency is not valid." },
                    { "15998103", "Rate card is not found." },
                    { "15998104", "Input data is not valid." },
                    { "15998600", "An internal server error happened." },
                    { "15999101", "Country code does not exist." },
                    { "15999102", "Currency is not valid." },
                    { "15999103", "Rate card is not found." },
                    { "15999104", "Input data is not valid." },
                    { "15999600", "An internal server error happened." },
                    { "16994101", "Input data is invalid." },
                    { "16994102", "Insufficient credit to callback." },
                    { "16994103", "Account not found." },
                    { "16994104", "Account is inactive, please contact to customer support." },
                    { "16994105", "Account has reached usage limit." },
                    { "16994106", "Rate calculation failed." },
                    { "16994107", "Callback execution failed." },
                    { "16994108", "Trunk not found." },
                    { "16994109", "Current user credentials not valid." },
                    { "16994600", "An Internal server error occured." },
                    { "16995101", "Input data is invalid." },
                    { "16995102", "Current user credentials not valid." },
                    { "16995600", "An Internal server error occured." },
                    { "17999101", "Client is invalid." },
                    { "17999102", "Culture is invalid. Example: 'en-US'." }
                },
                Success = true
            });
        }
    }
}