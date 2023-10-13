#nullable enable
namespace Appstract.Mobile.Application.Common.Models;
public class ApiResult<T>
{
    public T? Data { get; set; }
    public bool Success { get; set; }
    public ApiError? Error { get; set; }
}


