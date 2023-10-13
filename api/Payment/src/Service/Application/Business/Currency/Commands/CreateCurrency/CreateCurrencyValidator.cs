using FluentValidation;

namespace Application.Business.Currency.Commands.CreateCurrency;

public class CreateCurrencyValidator : AbstractValidator<CreateCurrencyRequest>
{
    public CreateCurrencyValidator()
    {
        RuleFor(c => c.Dto.Title).NotEmpty().NotNull().WithMessage("Invalid title");
    }
}