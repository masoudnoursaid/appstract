using ClientSdk.PaymentApi.V1;
using Payment.Sdk.Common.Extensions;
using Payment.Sdk.Common.Model;
using Payment.Sdk.Common.Model.Configuration;
using Payment.Sdk.Service.Connector;

namespace Payment.Sdk.Service.PayHubClient;

public sealed class PayHubClient : IPayHubClient
{
    private readonly PaymentConfiguration _configuration;
    private readonly IConnectorService _connectorService;

    public PayHubClient(PaymentConfiguration config
        , IConnectorService connectorService)
    {
        _configuration = config;
        _connectorService = connectorService;
    }


    public async Task<Result<CreatePaymentDto>> Pay(CreatePaymentPayload payload, string methodId)
    {
        payload.Validate();

        var result =
            await _connectorService.SendRequestToCreatePayment(payload, methodId,
                _configuration.SecurityConfiguration.ApiKey!);

        return result;
    }

    public async Task<Result<PaymentMethodList>> GetPaymentMethodList()
    {
        var result =
            await _connectorService.SendRequestToGetPaymentMethodList(null, null,
                _configuration.SecurityConfiguration.ApiKey!);

        return result;
    }
}