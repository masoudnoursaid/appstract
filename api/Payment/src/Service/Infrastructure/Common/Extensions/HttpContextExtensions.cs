using System.Web;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Infrastructure.Common.Extensions;

public static class HttpContextExtensions
{
    public static T ConvertFromQueryString<T>(this HttpContext context)
    {
        var responseString = context.Request.QueryString.ToString()!;
        var dict = HttpUtility.ParseQueryString(responseString);
        var json = JsonConvert.SerializeObject(dict.Cast<string>().ToDictionary(k => k, v => dict[v]));
        var result = JsonConvert.DeserializeObject<T>(json);
        return result!;
    }
}