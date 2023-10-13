using Application.Business.Currency.Dto;
using Application.Business.Transaction.Dto;
using Application.Common.BaseTypes.Dto;

namespace Application.Business.Payment.Queries.PaymentVisualize;

public record PaymentVisualizeDto(string PaymentId, string ProvidedId, string PaymentMethodName,
    string PaymentMethodIcon, string Amount, string ClientRedirectUrl, string StatusTitle, DateTime CompletedDate, CurrencyDto Currency,
    TransactionDto Transaction
    , string WebHookUrl
    ,[property: System.Text.Json.Serialization.JsonIgnore]
    [property: Newtonsoft.Json.JsonIgnore]
    string ApplicationSecret) : IBaseDto, IPaymentCostShowAble
{
    public string GetCost() => $"{Currency.Symbol}{Amount}";
}