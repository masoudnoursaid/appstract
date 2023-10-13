using App.SDK.Common.Enum;

namespace ClientSdk.Sample.AppApi.Models;

public class KycModel
{
    public KycModel(string token, string clientId, DateOnly date)
    {
        Token = token;
        ClientId = clientId;
        Date = date;
        status = KycProcessStatus.Waiting;
    }
    
    public string Token { get; set; }
    public DateOnly Date { get; set; }
    public KycProcessStatus status { get; set; }
    public string ClientId { get; set; }
}