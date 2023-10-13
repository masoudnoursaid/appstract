using Payment.Common.SDK.Models;
using Payment.Sdk.Common.Enum;

namespace Payment.Sdk.Common.Model;

public class Result<T> : Response<T, PaymentClientErrorCodes> where T : class
{
    public Result(T result)
    {
        Data = result;
        Success = true;
    }

    public Result(ClientPaymentErrorType code, params string[] messages)
    {
        Success = false;
        Error = PaymentError.Instance(code, new Dictionary<string, string>(
            new List<KeyValuePair<string, string>>
            {
                new("message", string.Join(",", messages))
            }
        ));
    }


    public static implicit operator Result<T>(T value)
    {
        return new Result<T>(value);
    }


    public static implicit operator Result<T>(ClientPaymentErrorType code)
    {
        return new Result<T>(code);
    }

    public static Result<T> Fail(ClientPaymentErrorType code, params string[] messages)
    {
        return new Result<T>(code, messages);
    }
}