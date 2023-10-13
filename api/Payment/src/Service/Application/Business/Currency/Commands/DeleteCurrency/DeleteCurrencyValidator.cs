using FluentValidation;

namespace Application.Business.Currency.Commands.DeleteCurrency;

public class DeleteCurrencyValidator : AbstractValidator<DeleteCurrencyRequest>
{
    public DeleteCurrencyValidator()
    {
        RuleFor(c => c.Id).NotEmpty().NotNull().WithMessage("Invalid Id");
    }
}