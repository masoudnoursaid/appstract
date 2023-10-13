using System.Globalization;
using System.Text;
using Application.Services.Payment;
using Application.Services.Payment.BillPlz;
using Application.Services.Payment.BillPlz.Payloads;
using Application.Services.Payment.BillPlz.ResponseModel;
using Application.Settings.Environments.BillPlz;
using Application.Settings.Environments.GlobalPaymentApi;
using Domain.Common.Util;
using Domain.Enums;
using Infrastructure.Services.BillPlz.Context;
using Infrastructure.Services.BillPlz.Extension;
using Infrastructure.Services.BillPlz.Model;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Infrastructure.Services.BillPlz;

public class BillPlzClientService : PaymentClientService<BillPlzContext>, IBillPlzClientService
{
    private const string BILL_V3_API = "api/v3/bills";

    private readonly IBillPlzEnvironmentSetting _billPlzEnvironmentSetting;
    private readonly IGlobalPaymentApiEnvironmentSetting _globalPaymentApiEnvironmentSetting;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public BillPlzClientService(IBillPlzEnvironmentSetting billPlzEnvironmentSetting,
        IGlobalPaymentApiEnvironmentSetting globalPaymentApiEnvironmentSetting,
        IHttpContextAccessor httpContextAccessor)
    {
        _billPlzEnvironmentSetting = billPlzEnvironmentSetting;
        _globalPaymentApiEnvironmentSetting = globalPaymentApiEnvironmentSetting;
        _httpContextAccessor = httpContextAccessor;
    }

    public SourceImplementedGetWay MyGetWayType { get; } = SourceImplementedGetWay.Billplz;

    public async Task<CreateBillPlzPaymentResponseModel> GeneratePaymentUrl(CreateBillPlzPaymentPayload payload,
        bool useSandbox = true)
    {
        var context = useSandbox ? SandBoxConfiguration() : LiveConfiguration();
        var response = await CreateBill(context
            , new BillPlzCreateBillModel(payload.Email
                , payload.Mobile
                , payload.Name
                , payload.Description
                , _billPlzEnvironmentSetting.SandboxCollectionId!
                , payload.Amount.ToString(CultureInfo.InvariantCulture)
                , _globalPaymentApiEnvironmentSetting.BillPlzUri.RedirectVerify.AbsoluteUri +
                  $"?paymentId={payload.PaymentId}"
                , _globalPaymentApiEnvironmentSetting.BillPlzUri.CallBackVerify.AbsoluteUri
                , payload.BankCode));

        return new CreateBillPlzPaymentResponseModel(response.PaymentUrl, response.ProvidedId);
    }

    public async Task<VerifyBillPlzPaymentResponseModel> VerifyPayment(VerifyBillPlzPaymentPayload payload,
        bool useSandbox = true)
    {
        var hmacModel = _httpContextAccessor.HttpContext.GetBillPlzHmacModel();
        var response = await VerifyHmac(hmacModel);
        return new VerifyBillPlzPaymentResponseModel($"BUILT_IN_BILLPLZ_{Guid.NewGuid()}", response.Verified);
    }

    protected override BillPlzContext SandBoxConfiguration()
    {
        var token = Base64OfApiKey(_billPlzEnvironmentSetting.SandboxApiKey!);
        var context = new BillPlzContext(token, false);
        return context;
    }

    protected override BillPlzContext LiveConfiguration()
    {
        var token = Base64OfApiKey(_billPlzEnvironmentSetting.ApiKey!);
        var context = new BillPlzContext(token, true);
        return context;
    }

    private static string Base64OfApiKey(string apiKey)
    {
        return Convert.ToBase64String(Encoding.ASCII.GetBytes(apiKey));
    }

    private async Task<BillPlzCreateBillResponseModel> CreateBill(BillPlzContext context, BillPlzCreateBillModel model)
    {
        var client = context.Client;

        var json = JsonConvert.SerializeObject(model);

        var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(BILL_V3_API, stringContent);

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<BillPlzCreateBillResponseModel>(content);

        return result!;
    }

    private Task<BillPlzVerifyHmacResponseModel> VerifyHmac(BillPlzHmacModel model)
    {
        var hmacStr = model.ToString();
        var hmac = HmacUtils.ComputeHMACSha256(_billPlzEnvironmentSetting.SandboxSignatureKey!, hmacStr);

        var verified = model.Signature.Equals(hmac, StringComparison.Ordinal);

        var result = new BillPlzVerifyHmacResponseModel(verified && bool.Parse(model.Paid));

        return Task.FromResult(result);
    }
}