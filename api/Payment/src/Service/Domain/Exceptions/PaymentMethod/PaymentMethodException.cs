using System.Runtime.Serialization;
using ErrorHandling.Abstracts;

namespace Domain.Exceptions.PaymentMethod;

public abstract class PaymentMethodException : AppException
{
    protected PaymentMethodException(string message)
        : base(message)
    {
    }

    protected PaymentMethodException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    protected PaymentMethodException(
        SerializationInfo info,
        StreamingContext context)
        : base(info, context)
    {
    }
}