using Payment.Sdk.Common.Attribute;

namespace Payment.Sdk.Common.Enum;

public enum PaymentClientErrorCodes
{
    [ClientError(ClientCommonErrorType.ClientException)]
    BadFormatOfApiSecret,

    [ClientError(ClientCommonErrorType.ClientException)]
    BadFormatOfApiKey,

    [ClientError(ClientCommonErrorType.NetworkException)]
    PingTimeout,

    [ClientError(ClientCommonErrorType.InternalServerError)]
    ApiKeyNotFound,

    [ClientError(ClientCommonErrorType.ClientException)]
    UnknownError,

    [ClientError(ClientCommonErrorType.BusinessLogic)]
    InvalidCurrencySymbol,

    [ClientError(ClientCommonErrorType.BusinessLogic)]
    InvalidPaymentMethodId
}