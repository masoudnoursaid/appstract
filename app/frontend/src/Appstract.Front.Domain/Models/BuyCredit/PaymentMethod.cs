using System.Text.Json.Serialization;

namespace Appstract.Front.Domain.Models.Financial;

public class PaymentMethod
{
    [JsonPropertyName("id")]
    public int PaymentMethodId { get; set; }
    public string? DisplayTitle { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public decimal? MinAmount { get; set; }
    public decimal? MaxAmount { get; set; }
    public List<long>? Amounts { get; set; } = new();
    public bool Enabled { get; set; }
    public ICollection<string>? Icon { get; set; } = new List<string>();
    public string? Provider { get; set; } = string.Empty;
    public string? Country { get; set; } = string.Empty;
    public string? CountryIcon { get; set; } = string.Empty;
    public bool Selected { get; set; }
    public decimal ExchangeRate { get; set; }
}