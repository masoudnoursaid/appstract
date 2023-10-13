using Mollie.Api.Models;
using Newtonsoft.Json;

namespace Infrastructure.Services.Mollie.Model;

public sealed class MollieCreatePaymentResponseModel
{
    [JsonProperty("id")] public string Id { get; set; } = null!;
    [JsonProperty("resource")] public string Resource { get; set; } = null!;
    [JsonProperty("mode")] public Mode Mode { get; set; }
    [JsonProperty("createdAt")] public DateTime? CreatedAt { get; set; }
    [JsonProperty("paidAt")] public DateTime? PaidAt { get; set; }
    [JsonProperty("failedAt")] public DateTime? FailedAt { get; set; }
    [JsonProperty("amount[value]")] public string Amount { get; set; } = null!;
    [JsonProperty("amount[currency]")] public string Currency { get; set; } = null!;
    [JsonProperty("orderId")] public string OrderId { get; set; } = null!;
    [JsonProperty("_links[checkout[href]]")] public string Checkout { get; set; } = null!;
}