using Application.Common.BaseTypes.Model;
using Application.Common.Payloads;

namespace Application.Services.Payment;

public interface IPaymentUrlVerifier<TResponse, in TPayload>
    where TPayload : IPayload
    where TResponse : BaseModel
{
    /// <summary>
    ///     Verify a payment
    /// </summary>
    /// <param name="payload">target dto to retrieve data from provider</param>
    /// <param name="useSandbox">true for test</param>
    /// <returns>verification status and payer information</returns>
    Task<TResponse> VerifyPayment(TPayload payload, bool useSandbox = true);
}

public interface IPaymentUrlGenerator<TResponse, in TPayload>
    where TPayload : IPayload
    where TResponse : BaseModel
{
    /// <summary>
    ///     Generate payment url
    /// </summary>
    /// <param name="payload">target dto to retrieve data from provider</param>
    /// <param name="useSandbox">true for test</param>
    /// <returns>url for payment and provided id which generate by provider</returns>
    Task<TResponse> GeneratePaymentUrl(TPayload payload, bool useSandbox = true);
}