using System.Net.NetworkInformation;
using ClientSdk.PaymentApi.V1;
using Microsoft.Extensions.Logging;
using Payment.Common.SDK.Service.Ping;
using Payment.Sdk.Common.Model;
using Payment.Sdk.Common.Model.Configuration;

namespace Payment.Sdk.Service.Connector;

public class ConnectorService : IConnectorService
{
    private readonly ConnectionConfiguration _connectionConfiguration;
    private readonly ILogger<ConnectorService> _logger;
    private readonly IPaymentApiClient _paymentApiClient;
    private readonly IPaymentMethodApiClient _paymentMethodApiClient;
    private readonly IPingService _pingService;

    public ConnectorService(PaymentConfiguration configuration
        , ILogger<ConnectorService> logger
        , IPingService pingService
        , IPaymentApiClient paymentApiClient
        , IPaymentMethodApiClient paymentMethodApiClient)
    {
        _connectionConfiguration = configuration.ConnectionConfiguration;
        _logger = logger;
        _pingService = pingService;
        _paymentApiClient = paymentApiClient;
        _paymentMethodApiClient = paymentMethodApiClient;
    }


    public async Task<Result<PaymentMethodList>> SendRequestToGetPaymentMethodList(int? page, int? perPage,
        string apiKey)
    {
        var ipStatus = await _pingService.PingServer(_connectionConfiguration.Uri);
        if (ipStatus == IPStatus.TimedOut)
            return ClientPaymentErrorType.PingTimeout;

        var result = await _paymentMethodApiClient.ListAsync(page, perPage, apiKey);


        if (result.Success)
        {
            var methodList = result.Data;

            _logger.LogInformation(
                "CONNECTOR_SERVICE.PAYMENT_METHOD_LIST --- Payment method list received successfully --- Count : {number}",
                methodList.Dtos.Count);
            return methodList;
        }

        var errorMessage = string.Join(',', result.Error.Values);
        _logger.LogError(
            "CONNECTOR_SERVICE.PAYMENT_METHOD_LIST --- Error code : {code} --- Error type : {type} --- Values : {msg}",
            result.Error.Code, result.Error.Type, errorMessage);
        return result.Error.Code switch
        {
            _ => Result<PaymentMethodList>.Fail(ClientPaymentErrorType.UnknownError, errorMessage)
        };
    }

    public async Task<Result<CreatePaymentDto>> SendRequestToCreatePayment(CreatePaymentPayload payload,
        string methodId, string apiKey)
    {
        var ipStatus = await _pingService.PingServer(_connectionConfiguration.Uri);
        if (ipStatus == IPStatus.TimedOut)
            return ClientPaymentErrorType.PingTimeout;

        var result = await _paymentApiClient.CreateAsync(apiKey,
            new CreatePaymentRequest
            {
                Payload = payload,
                PaymentMethodId = methodId
            });


        if (result.Success)
        {
            var paymentDto = result.Data;

            _logger.LogInformation(
                "CONNECTOR_SERVICE.CREATE_PAYMENT --- Payment intent created successfully --- PaymentId : {paymentId} --- ProvidedId : {providedId} --- Pay Url : {url}",
                paymentDto.PaymentId, paymentDto.ProvidedId, paymentDto.PaymentUrl);
            return paymentDto;
        }

        var errorMessage = string.Join(',', result.Error.Values);
        _logger.LogError(
            "CONNECTOR_SERVICE.PAYMENT_METHOD_LIST --- Error code : {code} --- Error type : {type} --- Values : {msg}",
            result.Error.Code, result.Error.Type, errorMessage);
        return result.Error.Code switch
        {
            CreatePaymentErrorCodes.CurrencyNotFound => Result<CreatePaymentDto>.Fail(
                ClientPaymentErrorType.InvalidCurrencySymbol, errorMessage),
            CreatePaymentErrorCodes.MethodNotFound => Result<CreatePaymentDto>.Fail(
                ClientPaymentErrorType.InvalidPaymentMethodId, errorMessage),
            _ => Result<CreatePaymentDto>.Fail(ClientPaymentErrorType.UnknownError,
                errorMessage)
        };
    }
}