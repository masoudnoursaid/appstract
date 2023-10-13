using ErrorHandling;
using MediatR;
using Microsoft.Extensions.Logging;
using Payment.Sdk.Service.PayHubClient;
using PaymentMethodListDto = ClientSdk.PaymentApi.V1.PaymentMethodList;

namespace Application.Payment.Queries.PaymentMethodList;

public class
    GetPaymentMethodListRequestHandler : IRequestHandler<GetPaymentMethodListRequest, Response<PaymentMethodListDto>>
{
    private readonly IPayHubClient _payHubClient;
    private readonly ILogger<GetPaymentMethodListRequestHandler> _logger;

    public GetPaymentMethodListRequestHandler(IPayHubClient payHubClient,
        ILogger<GetPaymentMethodListRequestHandler> logger)
    {
        _payHubClient = payHubClient;
        _logger = logger;
    }

    public async Task<Response<PaymentMethodListDto>> Handle(GetPaymentMethodListRequest request,
        CancellationToken cancellationToken)
    {
        var hubResponse = await _payHubClient.GetPaymentMethodList();

        if (!hubResponse.Success)
        {
            var msg = string.Join(",", hubResponse.Error!.Values!);
            _logger.LogError(
                "GET_PAYMENT_METHOD_LIST_REQUEST_HANDLER.HANDLER --- Get payment method list failed with code : {code} --- Message : {msg}",
                hubResponse.Error.Code, msg);
        }

        return hubResponse.Data!;
    }
}