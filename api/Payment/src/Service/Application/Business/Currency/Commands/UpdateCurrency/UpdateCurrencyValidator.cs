using FluentValidation;

namespace Application.Business.Currency.Commands.UpdateCurrency;

public class UpdateCurrencyValidator : AbstractValidator<UpdateCurrencyRequest>
{
    public UpdateCurrencyValidator()
    {
        RuleFor(r => r.Id).NotEmpty().NotNull().WithMessage("Invalid Id");
    }
}