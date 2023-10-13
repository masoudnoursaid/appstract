namespace Payment.Sdk.Exceptions;

public class InvalidPaymentPayloadException : Exception
{
    public InvalidPaymentPayloadException(string propertyName) : base(
        $"Property {propertyName} in payment payload is invalid, please fill all properties")
    {
    }
}