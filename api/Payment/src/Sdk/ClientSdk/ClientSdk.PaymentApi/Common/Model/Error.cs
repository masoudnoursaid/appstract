using Payment.Common.SDK.Models;
using Payment.Sdk.Common.Enum;

namespace Payment.Sdk.Common.Model;

public class Error : ErrorBase<PaymentClientErrorCodes>
{
    private Error(ClientPaymentErrorType code
        , ClientCommonErrorType type
        , Dictionary<string, string>? values = null) : base(code, type, values)
    {
    }


    public static Error Instance(ClientPaymentErrorType code
        , ClientCommonErrorType type
        , Dictionary<string, string>? values = null)
    {
        return new Error(code, type, values);
    }

    public static Error Instance(ClientPaymentErrorType code
        , Dictionary<string, string>? values = null)
    {
        var type = ClientCommonErrorType.InternalServerError;
        return new Error(code, type, values);
    }
}