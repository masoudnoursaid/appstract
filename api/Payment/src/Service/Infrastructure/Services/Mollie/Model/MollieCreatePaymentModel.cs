using Newtonsoft.Json;

namespace Infrastructure.Services.Mollie.Model;

public sealed class MollieCreatePaymentModel
{
    [JsonProperty("amount[value]")] public string Amount { get; set; } = null!;
    [JsonProperty("amount[currency]")] public string Currency { get; set; } = null!;
    [JsonProperty("description")] public string Description { get; set; } = null!;
    [JsonProperty("redirectUrl")] public string RedirectUrl { get; set; } = null!;
    [JsonProperty("cancelUrl")] public string CancelUrl { get; set; } = null!;
    [JsonProperty("webhookUrl")] public string WebhookUrl { get; set; } = null!;
    [JsonProperty("customerId")] public string CustomerId { get; set; } = null!;
    [JsonProperty("testMode")] public bool? TestMode { get; set; }
}