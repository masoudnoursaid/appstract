using System.Runtime.Serialization;
using ErrorHandling.Attributes;

namespace ErrorHandling.Exceptions;

/// <summary>
///     Error codes enum field must be annotated with <see cref="ErrorTypeAttribute" />.
/// </summary>
[Serializable]
public class ErrorTypeAttributeIsMissingException : Exception
{
    public ErrorTypeAttributeIsMissingException(Enum enumValue)
        : base($"'{enumValue.ToString()}' value of '{enumValue.GetType()}' enum must be annotated " +
               $"with '{nameof(ErrorTypeAttribute)}'.")
    {
    }

    protected ErrorTypeAttributeIsMissingException(
        SerializationInfo info,
        StreamingContext context)
        : base(info, context)
    {
    }
}