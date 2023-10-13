using ErrorHandling.Enums;

namespace ErrorHandling.Attributes;

[AttributeUsage(AttributeTargets.Enum | AttributeTargets.Class)]
public class HandlerCodeAttribute : Attribute
{
    public HandlerCodeAttribute(HandlerCode handlerCode)
    {
        HandlerCode = handlerCode;
    }

    public HandlerCode HandlerCode { get; }
}