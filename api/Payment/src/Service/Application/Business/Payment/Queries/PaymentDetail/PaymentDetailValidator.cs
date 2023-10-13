using FluentValidation;

namespace Application.Business.Payment.Queries.PaymentDetail;

public class PaymentDetailValidator : AbstractValidator<PaymentDetailRequest>
{
    public PaymentDetailValidator()
    {
        RuleFor(r => r.PaymentId).NotEmpty().NotNull().WithMessage("Invalid PaymentId");
    }
}