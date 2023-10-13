using FluentValidation;

namespace Application.Business.PaymentMethod.Commands.UpdatePaymentMethod;

public class UpdatePaymentMethodValidator : AbstractValidator<UpdatePaymentMethodRequest>
{
    public UpdatePaymentMethodValidator()
    {
        RuleFor(r => r.Id).NotEmpty().NotNull().WithMessage("Invalid Id");
    }
}