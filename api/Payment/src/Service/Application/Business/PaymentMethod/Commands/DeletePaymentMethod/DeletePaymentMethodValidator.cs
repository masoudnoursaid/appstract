using FluentValidation;

namespace Application.Business.PaymentMethod.Commands.DeletePaymentMethod;

public class DeletePaymentMethodValidator : AbstractValidator<DeletePaymentMethodRequest>
{
    public DeletePaymentMethodValidator()
    {
        RuleFor(r => r.Id).NotEmpty().NotNull().WithMessage("Invalid Id");
    }
}