using Application.Repositories.Generic.Create;
using ClientSdk.PaymentApi.V1;
using ErrorHandling;
using MediatR;
using Microsoft.Extensions.Logging;
using Payment.Sdk.Service.PayHubClient;

namespace Application.Payment.Commands.CreatePayment;

public class CreatePaymentRequestHandler : IRequestHandler<CreatePaymentRequest, Response<CreatePaymentDto>>
{
    private readonly IPayHubClient _payHubClient;
    private readonly ICreateRepository<PaymentEntity> _paymentCreateRepository;
    private readonly ILogger<CreatePaymentRequestHandler> _logger;

    public CreatePaymentRequestHandler(IPayHubClient payHubClient,
        ICreateRepository<PaymentEntity> paymentCreateRepository, ILogger<CreatePaymentRequestHandler> logger)
    {
        _paymentCreateRepository = paymentCreateRepository;
        _payHubClient = payHubClient;
        _logger = logger;
    }

    public async Task<Response<CreatePaymentDto>> Handle(CreatePaymentRequest request,
        CancellationToken cancellationToken)
    {
        var hubResponse = await _payHubClient.Pay(request.Payload, request.PaymentMethodId);

        if (!hubResponse.Success)
        {
            var msg = string.Join(",", hubResponse.Error!.Values!);
            _logger.LogError(
                "CREATE_PAYMENT_REQUEST_HANDLER.HANDLER --- Create payment failed with code : {code} --- Message : {msg}",
                hubResponse.Error.Code, msg);

            return CreatePaymentErrorCodes.PayHubFailed;
        }

        CreatePaymentDto data = hubResponse.Data!;

        await _paymentCreateRepository.Insert(new PaymentEntity()
        {
            PaymentMethodId = request.PaymentMethodId,
            Amount = request.Payload.Amount,
            ProvidedId = data.ProvidedId,
            PayHubPaymentId = data.PaymentId
        });

        return hubResponse.Data!;
    }
}