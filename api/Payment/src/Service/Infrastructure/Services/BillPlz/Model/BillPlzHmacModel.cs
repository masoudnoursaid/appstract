using System.Reflection;
using Application.Common.Attributes;
using Newtonsoft.Json;

namespace Infrastructure.Services.BillPlz.Model;

public sealed class BillPlzHmacModel
{
    [JsonProperty("billplz[id]")] public string BillPlzId { get; set; } = null!;
    [JsonProperty("billplz[paid]")] public string Paid { get; set; } = null!;
    [JsonProperty("billplz[paid_at]")] public string PaidAt { get; set; } = null!;

    [JsonProperty("billplz[x_signature]")]
    [Exclude]
    public string Signature { get; set; } = null!;

    public override string ToString()
    {
        var props = typeof(BillPlzHmacModel).GetProperties()
            .Where(p => p.GetCustomAttribute<ExcludeAttribute>() == null)
            .OrderBy(p => p.GetCustomAttribute<JsonPropertyAttribute>()!.PropertyName).ToList();
        var result = string.Empty;
        foreach (var prop in props)
        {
            var propertyName = prop.GetCustomAttribute<JsonPropertyAttribute>()!.PropertyName;
            if (propertyName != null)
            {
                var name = propertyName.Replace("[", "").Replace("]", "")
                    .ToLower();
                result += $"{name}{prop.GetValue(this)}|";
            }
        }

        result = result.TrimEnd('|');
        return result;
    }
}