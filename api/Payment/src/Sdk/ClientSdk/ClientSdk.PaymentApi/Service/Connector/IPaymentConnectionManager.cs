using ClientSdk.PaymentApi.V1;
using Payment.Sdk.Common.Model;

namespace Payment.Sdk.Service.Connector;

public interface IPaymentConnectionManager
{
    Task<Result<CreatePaymentDto>>
        SendRequestToCreatePayment(CreatePaymentPayload payload, string methodId, string apiKey);
}