namespace Payment.Sdk.Common.Attribute;

[AttributeUsage(AttributeTargets.Field)]
public class ClientErrorAttribute : System.Attribute
{
    public ClientErrorAttribute(ClientCommonErrorType type)
    {
        ClientErrorType = type;
    }

    public ClientCommonErrorType ClientErrorType { get; set; }
}