using Application.Common.BaseTypes.Context;

namespace Infrastructure.Services.BillPlz.Context;

public class BillPlzContext : IPaymentContext
{
    private const string SANDBOX_URL = "https://www.billplz-sandbox.com";
    private const string LIVE_URL = "https://www.billplz.com";

    public BillPlzContext(string token, bool isLive)
    {
        IsLive = isLive;
        Token = token;
        Client = new HttpClient
        {
            BaseAddress = new Uri(IsLive ? LIVE_URL : SANDBOX_URL)
        };
        Client.DefaultRequestHeaders.Add("Authorization", $"Basic {token}");
    }

    public HttpClient Client { get; set; }
    public bool IsLive { get; set; }
    public string Token { get; set; }
}