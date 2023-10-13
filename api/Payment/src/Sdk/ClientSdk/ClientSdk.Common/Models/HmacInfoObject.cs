namespace Payment.Common.SDK.Models;

public class HmacInfoObject
{
    public HmacInfoObject(Uri uri, HttpMethod method)
    {
        Ticks = DateTime.UtcNow.Ticks;
        Nonce = Guid.NewGuid().ToString();
        Method = method;
        Uri = uri;
    }

    public HmacInfoObject(Uri uri, HttpMethod method, long ticks, string nonce) : this(uri, method)
    {
        Ticks = ticks;
        Nonce = nonce;
    }


    public long Ticks { get; }
    public string Nonce { get; }
    public Uri Uri { get; set; }
    public HttpMethod Method { get; set; }

    public override string ToString()
    {
        var template = new HmacTemplate();
        return template.Render(Nonce, Uri.Host, Uri.PathAndQuery, Method.ToString(), Ticks.ToString());
    }
}