using Newtonsoft.Json;

namespace Infrastructure.Services.BillPlz.Model;

public sealed class BillPlzCreateBillModel
{
    public BillPlzCreateBillModel(string email, string mobile, string name, string description, string collectionId,
        string amount, string redirectUrl, string callBackUrl, string bankCode)
    {
        Email = email;
        Mobile = mobile;
        Name = name;
        Description = description;
        CollectionId = collectionId;
        Amount = amount;
        RedirectUrl = redirectUrl;
        CallBackUrl = callBackUrl;
        BankCode = bankCode;
    }

    [JsonProperty("email")] public string Email { get; set; }
    [JsonProperty("mobile")] public string Mobile { get; set; }
    [JsonProperty("name")] public string Name { get; set; }
    [JsonProperty("description")] public string Description { get; set; }
    [JsonProperty("collection_id")] public string CollectionId { get; set; }
    [JsonProperty("amount")] public string Amount { get; set; }
    [JsonProperty("redirect_url")] public string RedirectUrl { get; set; }
    [JsonProperty("callback_url")] public string CallBackUrl { get; set; }
    [JsonProperty("reference_1")] public string BankCode { get; set; }

    [JsonProperty("reference_1_label")] public string Ref => "Bank Code";
}