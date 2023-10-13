using Newtonsoft.Json;

namespace Infrastructure.Services.Paypal.Model;

public sealed class PaypalPayerInfoModel
{
    [JsonProperty("payerId")] public string PayerId { get; set; } = null!;
}