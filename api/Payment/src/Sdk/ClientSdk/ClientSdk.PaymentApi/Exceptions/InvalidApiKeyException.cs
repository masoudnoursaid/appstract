namespace Payment.Sdk.Exceptions;

public class InvalidApiKeyException : Exception
{
    public InvalidApiKeyException(string apiKey) : base($"Invalid api key : {apiKey}")
    {
    }
}