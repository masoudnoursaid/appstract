using ErrorHandling;
using ErrorHandling.Attributes;
using ErrorHandling.Enums;
using MediatR;

namespace Application.Business.Payment.Queries.PaymentDetail;

[HandlerCode(HandlerCode.PaymentDetail)]
public record PaymentDetailRequest(string PaymentId, bool IncludeAllDependencies = true,
    string? NavigationPropertyPath = null) : IRequest<Response<PaymentDetailDto>>;