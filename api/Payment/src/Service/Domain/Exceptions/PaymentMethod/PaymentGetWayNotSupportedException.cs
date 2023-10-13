using System.Runtime.Serialization;
using Domain.Enums;
using ErrorHandling.Enums;
using ErrorHandling.Interfaces;

namespace Domain.Exceptions.PaymentMethod;

[Serializable]
public class PaymentGetWayNotSupportedException : PaymentMethodException, ICodedException
{
    public PaymentGetWayNotSupportedException(SourceImplementedGetWay getWay) : base(
        $"GetWay {getWay} is not implemented yet")
    {
    }

    public PaymentGetWayNotSupportedException(SourceImplementedGetWay getWay, Exception innerException) : base(
        $"GetWay {getWay} is not implemented yet", innerException)
    {
    }

    protected PaymentGetWayNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public CommonErrorCode GetCommonErrorCode()
    {
        return CommonErrorCode.GetWayNotImplementedYet;
    }
}