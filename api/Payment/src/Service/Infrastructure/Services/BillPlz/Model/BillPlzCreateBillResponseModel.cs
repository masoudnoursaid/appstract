using Newtonsoft.Json;

namespace Infrastructure.Services.BillPlz.Model;

public sealed class BillPlzCreateBillResponseModel
{
    [JsonProperty("id")] public string ProvidedId { get; set; } = null!;
    [JsonProperty("url")] public string PaymentUrl { get; set; } = null!;
}