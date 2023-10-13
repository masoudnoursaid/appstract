using Application.Services.Payment;
using Application.Services.Payment.Mollie;
using Application.Services.Payment.Mollie.Payloads;
using Application.Services.Payment.Mollie.ResponseModel;
using Application.Settings.Environments.GlobalPaymentApi;
using Application.Settings.Environments.Mollie;
using Domain.Enums;
using Infrastructure.Services.Mollie.Context;
using Infrastructure.Services.Mollie.Extension;
using Infrastructure.Services.Mollie.Model;
using Mollie.Api.Client;
using Mollie.Api.Client.Abstract;
using Mollie.Api.Models.Payment.Response;

namespace Infrastructure.Services.Mollie;

public class MollieClientService : PaymentClientService<MollieContext>, IMollieClientService
{
    private readonly IMollieEnvironmentSetting _mollieEnvironmentSetting;
    private readonly IGlobalPaymentApiEnvironmentSetting _globalPaymentApiEnvironmentSetting;

    public MollieClientService(IMollieEnvironmentSetting mollieEnvironmentSetting,
        IGlobalPaymentApiEnvironmentSetting globalPaymentApiEnvironmentSetting)
    {
        _mollieEnvironmentSetting = mollieEnvironmentSetting;
        _globalPaymentApiEnvironmentSetting = globalPaymentApiEnvironmentSetting;
    }

    public SourceImplementedGetWay MyGetWayType => SourceImplementedGetWay.Mollie;

    public async Task<CreateMolliePaymentResponseModel> GeneratePaymentUrl(CreateMolliePaymentPayload payload,
        bool useSandbox = true)
    {
        MollieContext context = useSandbox ? SandBoxConfiguration() : LiveConfiguration();

        var paymentModel = payload.ToMollieCreatePayment(_globalPaymentApiEnvironmentSetting, !context.IsLive);
        var response = await CreatePayment(context, paymentModel);

        return new CreateMolliePaymentResponseModel(response.Checkout, response.Id);
    }

    public async Task<VerifyMolliePaymentResponseModel> VerifyPayment(VerifyMolliePaymentPayload payload,
        bool useSandbox = true)
    {
        MollieContext context = useSandbox ? SandBoxConfiguration() : LiveConfiguration();

        var response = await CheckStatusOfPayment(context, payload.ProvidedPaymentId);

        return new VerifyMolliePaymentResponseModel(response.PayerId, response.Verified);
    }

    protected override MollieContext SandBoxConfiguration() => new(_mollieEnvironmentSetting.SandboxApiKey!, false);

    protected override MollieContext LiveConfiguration() => new(_mollieEnvironmentSetting.ApiKey!, true);


    private async Task<MollieCreatePaymentResponseModel> CreatePayment(MollieContext context,
        MollieCreatePaymentModel model)
    {
        using IPaymentClient paymentClient = new PaymentClient(context.ApiKey, context.MollieHttpClient);

        var paymentRequest = model.ToPaymentRequest();
        PaymentResponse response = await paymentClient.CreatePaymentAsync(paymentRequest);

        return response.ToCreatePaymentResponse();
    }

    private async Task<PaymentStatusModel> CheckStatusOfPayment(MollieContext context, string paymentId)
    {
        using IPaymentClient paymentClient = new PaymentClient(context.ApiKey, context.MollieHttpClient);

        PaymentResponse response = await paymentClient.GetPaymentAsync(paymentId);

        var verified = response.PaidAt != null;
        return new PaymentStatusModel(verified, response.CustomerId);
    }
}