namespace ClientSdk.Common.Models;

public abstract class Response<TData, TErrorEnum>
    where TErrorEnum : Enum
{
    protected Response()
    {
    }

    protected Response(TData data)
    {
        Success = true;
        Data = data;
    }

    protected Response(ErrorBase<TErrorEnum> error)
    {
        Success = false;
        Error = error;
    }

    public bool Success { get; set; }
    public TData? Data { get; set; }
    public ErrorBase<TErrorEnum>? Error { get; set; }
}