using ClientSdk.PaymentApi.V1;
using Payment.Sdk.Common.Model;

namespace Payment.Sdk.Service.PayHubClient;

public interface IPayment
{
    Task<Result<CreatePaymentDto>> Pay(CreatePaymentPayload payload, string methodId);
}