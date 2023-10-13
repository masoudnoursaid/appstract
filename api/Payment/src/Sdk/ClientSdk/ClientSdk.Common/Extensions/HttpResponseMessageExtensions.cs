using Newtonsoft.Json;
using Payment.Common.SDK.Models;

namespace Payment.Common.SDK.Extensions;

public static class HttpResponseMessageExtensions
{
    public static async Task<TResponse> DeserializeAsync<TResponse>(this HttpResponseMessage httpResponse)
    {
        var content = await httpResponse.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TResponse>(content)!;
    }

    public static async Task<Response<TData, TErrorEnum>> DeserializeAsync<TData, TErrorEnum>(
        this HttpResponseMessage httpResponse)
        where TErrorEnum : Enum
    {
        var content = await httpResponse.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Response<TData, TErrorEnum>>(content)!;
    }
}