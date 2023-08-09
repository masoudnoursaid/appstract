using ErrorHandling.Attributes;
using ErrorHandling.Enums;

namespace Application.Account.Queries.GetInfo;

public enum GetInfoErrorCodes
{
    [ErrorType(BackendErrorType.BusinessLogic)]
    AgeIsLessThan20 = 11_999_101
}