namespace Payment.Sdk.Exceptions;

public class InvalidApiSecretException : Exception
{
    public InvalidApiSecretException(string apiSecret) : base($"Invalid api key : {apiSecret}")
    {
    }
}