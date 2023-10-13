using ClientSdk.PaymentApi.V1;
using Payment.Sdk.Common.Model;

namespace Payment.Sdk.Service.PayHubClient;

public interface IPaymentMethod
{
    Task<Result<PaymentMethodList>> GetPaymentMethodList();
}