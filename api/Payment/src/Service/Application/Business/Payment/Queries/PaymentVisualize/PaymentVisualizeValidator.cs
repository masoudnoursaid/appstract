using FluentValidation;

namespace Application.Business.Payment.Queries.PaymentVisualize;

public class PaymentVisualizeValidator : AbstractValidator<PaymentVisualizeRequest>
{
    public PaymentVisualizeValidator()
    {
        RuleFor(r => r.PaymentId).NotNull().NotEmpty().WithMessage("Invalid PaymentId");
    }
}