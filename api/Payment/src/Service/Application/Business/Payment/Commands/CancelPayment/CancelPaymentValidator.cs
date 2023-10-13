using FluentValidation;

namespace Application.Business.Payment.Commands.CancelPayment;

public class CancelPaymentValidator : AbstractValidator<CancelPaymentRequest>
{
    public CancelPaymentValidator()
    {
        RuleFor(r => r.PaymentId).NotEmpty().NotNull().WithMessage("Invalid PaymentId");
    }
}