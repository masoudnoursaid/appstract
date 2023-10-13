namespace Appstract.Mobile.Application.Common.Models;

public class ApiError
{
    public ApiError()
    {
    }

    public ApiError(int code, int type, Dictionary<string, string> values)
    {
        Code = code;
        Type = type;
        Values = values;
    }

    public int? Code { get; set; }
    public int? Type { get; set; }
    public IDictionary<string, string>? Values { get; set; }
}


