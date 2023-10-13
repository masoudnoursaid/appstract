using ClientSdk.PaymentApi.V1;
using Payment.Sdk.Common.Model;

namespace Payment.Sdk.Service.Connector;

public interface IPaymentMethodConnectionManager
{
    Task<Result<PaymentMethodList>>
        SendRequestToGetPaymentMethodList(int? page, int? perPage, string apiKey);
}