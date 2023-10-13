namespace Payment.Sdk.Common.Model.Configuration;

public class PaymentConfiguration
{
    public SecurityConfiguration SecurityConfiguration { get; set; } = new();
    public ConnectionConfiguration ConnectionConfiguration { get; set; } = new();
}