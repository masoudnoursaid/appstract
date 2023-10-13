using System.Text.Json.Serialization;

namespace Appstract.Front.Domain.Models.ApiResponseModels;

public class ApiResponseBase<TResult>
    where TResult : class, new()
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }
    [JsonPropertyName("data")]
    public TResult Data { get; set; } = new();
    [JsonPropertyName("error")]
    public Error Error { get; set; } = new();
}