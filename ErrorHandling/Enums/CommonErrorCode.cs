using ErrorHandling.Attributes;

namespace ErrorHandling.Enums;

/// <summary>
/// 3 digit error codes for common errors which are not specific to any request.
/// Instead of creating a new error code for each request, these common error codes are used in the pipeline behavior
/// by appending these 3 digit codes to the request (handler) code.
/// </summary>
public enum CommonErrorCode
{
    [ErrorType(BackendErrorType.ApplicationFailure)]
    UnexpectedError = 600,

    [ErrorType(BackendErrorType.ApplicationFailure)]
    InvalidIdentity = 601,

    [ErrorType(BackendErrorType.ApplicationFailure)]
    CcCardNotFound = 602,

    [ErrorType(BackendErrorType.ApplicationFailure)]
    InvalidIpAddress = 603,

    [ErrorType(BackendErrorType.ApplicationFailure)]
    UnableToCalculateExchangeRate = 604,

    [ErrorType(BackendErrorType.ApplicationFailure)]
    GeoIpServiceFailed = 605,

    [ErrorType(BackendErrorType.ApplicationFailure)]
    AuthTokenGenerationFailure = 606,

    [ErrorType(BackendErrorType.ApplicationFailure)]
    SsoSessionNotFound = 607,

    [ErrorType(BackendErrorType.ApplicationFailure)]
    InvalidSsoSession = 608,

    [ErrorType(BackendErrorType.ApplicationFailure)]
    DatabaseError = 700,

    [ErrorType(BackendErrorType.ApplicationFailure)]
    DatabaseConnectionFailed = 701,

    [ErrorType(BackendErrorType.ApplicationFailure)]
    SaveChangesFailed = 702,

    [ErrorType(BackendErrorType.ApplicationFailure)]
    DbUpdateConcurrencyFailed = 703,

    [ErrorType(BackendErrorType.Security)]
    ValidationFailed = 800,

    [ErrorType(BackendErrorType.BusinessLogic)]
    AccountIsSuspended = 801,

    [ErrorType(BackendErrorType.BusinessLogic)]
    IncompleteUser = 802,

    [ErrorType(BackendErrorType.BusinessLogic)]
    InvalidCountryCode = 803,

    [ErrorType(BackendErrorType.Security)]
    ModelBindingFailed = 804,

    [ErrorType(BackendErrorType.BusinessLogic)]
    SessionRevoked = 805,
}