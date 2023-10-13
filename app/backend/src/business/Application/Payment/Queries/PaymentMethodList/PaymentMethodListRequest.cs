using ErrorHandling;
using ErrorHandling.Attributes;
using ErrorHandling.Enums;
using MediatR;
using PaymentMethodListDto = ClientSdk.PaymentApi.V1.PaymentMethodList;

namespace Application.Payment.Queries.PaymentMethodList;

[HandlerCode(HandlerCode.GetPaymentMethodList)]
public record GetPaymentMethodListRequest : IRequest<Response<PaymentMethodListDto>>;
