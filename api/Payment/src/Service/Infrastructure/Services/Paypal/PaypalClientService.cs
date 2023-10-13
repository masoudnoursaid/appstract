using System.Globalization;
using Application.Services.Payment;
using Application.Services.Payment.Paypal;
using Application.Services.Payment.Paypal.Payloads;
using Application.Services.Payment.Paypal.ResponseModel;
using Application.Settings.Environments.GlobalPaymentApi;
using Application.Settings.Environments.Paypal;
using Domain.Enums;
using Domain.ValueObjects.Payment;
using Infrastructure.Services.Paypal.Context;
using Infrastructure.Services.Paypal.Extension;
using Microsoft.AspNetCore.Http;
using PayPal.Api;
using PaypalPayment = PayPal.Api.Payment;

namespace Infrastructure.Services.Paypal;

public class PaypalClientService : PaymentClientService<PaypalContext>, IPaypalClientService
{
    private const string MODE = "mode";
    private const string SAND_BOX = "sandbox";
    private const string LIVE = "live";
    private const string APPROVED = "approved";
    private readonly IGlobalPaymentApiEnvironmentSetting _globalPaymentApiEnvironmentSetting;
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly IPaypalEnvironmentSetting _paypalEnvironmentSetting;

    public PaypalClientService(IPaypalEnvironmentSetting paypalEnvironmentSetting
        , IGlobalPaymentApiEnvironmentSetting globalPaymentApiEnvironmentSetting,
        IHttpContextAccessor httpContextAccessor)
    {
        _paypalEnvironmentSetting = paypalEnvironmentSetting;
        _globalPaymentApiEnvironmentSetting = globalPaymentApiEnvironmentSetting;
        _httpContextAccessor = httpContextAccessor;
    }

    public SourceImplementedGetWay MyGetWayType { get; } = SourceImplementedGetWay.Paypal;

    public async Task<CreatePaypalPaymentResponseModel> GeneratePaymentUrl(CreatePaypalPaymentPayload payload,
        bool useSandbox = true)
    {
        APIContext context = useSandbox ? SandBoxConfiguration() : LiveConfiguration();
        var payment = await CreatePayment(context
            , payload.Items
            , payload.PaymentId
            , payload.Currency
            , payload.Amount
            , payload.InvoiceNumber
            , payload.Description);

        return new CreatePaypalPaymentResponseModel(payment.GetApprovalUrl(), payment.id);
    }

    public async Task<VerifyPaypalPaymentResponseModel> VerifyPayment(VerifyPaypalPaymentPayload payload,
        bool useSandbox = true)
    {
        APIContext context = useSandbox ? SandBoxConfiguration() : LiveConfiguration();
        var payer = _httpContextAccessor.HttpContext.GetPaypalPayerInfoModel();
        var payment = await ExecutePayment(context, payer.PayerId, payload.ProvidedPaymentId);

        return new VerifyPaypalPaymentResponseModel(payment.payer.payer_info.payer_id, IsVerified(payment.state));
    }


    protected override PaypalContext SandBoxConfiguration()
    {
        var config = new Dictionary<string, string>
        {
            { MODE, SAND_BOX }
        };
        var token = new OAuthTokenCredential(_paypalEnvironmentSetting.SandBoxClientId,
            _paypalEnvironmentSetting.SandBoxClientSecret,
            config).GetAccessToken();
        var context = new PaypalContext(token)
        {
            Config = config,
            HTTPHeaders = new Dictionary<string, string>
            {
                { "Content-Type", "application/json" }
            }
        };
        return context;
    }

    protected override PaypalContext LiveConfiguration()
    {
        var config = new Dictionary<string, string>
        {
            { MODE, LIVE }
        };
        var token = new OAuthTokenCredential(_paypalEnvironmentSetting.LiveClientId,
            _paypalEnvironmentSetting.LiveClientSecret,
            config).GetAccessToken();
        var context = new PaypalContext(token)
        {
            Config = config,
            HTTPHeaders = new Dictionary<string, string>
            {
                { "Content-Type", "application/json" }
            }
        };
        return context;
    }

    private Task<PaypalPayment> CreatePayment(APIContext context, IEnumerable<PaymentItem> items,
        string paymentId,
        string currency,
        float amount,
        string invoiceNumber,
        string description)
    {
        var itemList = new ItemList
        {
            items = new List<Item>()
        };
        itemList.items.AddRange(items.Select(i => new Item
        {
            name = i.Name,
            currency = i.Currency,
            price = i.Price,
            quantity = i.Quantity.ToString(),
            sku = i.Sku
        }).ToList());

        var payer = new Payer
        {
            payment_method = "paypal"
        };

        var redirectUrls = new RedirectUrls
        {
            cancel_url = _globalPaymentApiEnvironmentSetting.PaypalUri.Cancel.AbsoluteUri +
                         $"?id={paymentId}",
            return_url = _globalPaymentApiEnvironmentSetting.PaypalUri.Verify.AbsoluteUri +
                         $"?id={paymentId}"
        };

        var payAmount = new Amount
        {
            currency = currency,
            total = amount.ToString(CultureInfo.InvariantCulture)
        };


        var transactionList = new List<Transaction>
        {
            new()
            {
                description = description,
                invoice_number = Guid.NewGuid().ToString(),
                amount = payAmount,
                item_list = itemList
            }
        };

        var payment = new PaypalPayment
        {
            intent = "sale",
            payer = payer,
            transactions = transactionList,
            redirect_urls = redirectUrls
        };

        var obj = payment.Create(context);

        return Task.FromResult(obj);
    }

    private Task<PaypalPayment> ExecutePayment(APIContext context, string payerId, string paymentId)
    {
        var paymentExecution = new PaymentExecution
        {
            payer_id = payerId
        };

        var payment = new PaypalPayment
        {
            id = paymentId
        };

        var result = payment.Execute(context, paymentExecution);

        return Task.FromResult(result);
    }

    private static bool IsVerified(string state)
    {
        return state.Equals(APPROVED, StringComparison.OrdinalIgnoreCase);
    }
}