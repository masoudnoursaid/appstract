using Application.Common.Regex;
using FluentValidation;

namespace Application.Payment.Commands.CreatePayment;

public class CreatePaymentValidator : AbstractValidator<CreatePaymentRequest>
{
    public CreatePaymentValidator()
    {
        RuleFor(r => r.PaymentMethodId).NotEmpty().NotNull().WithMessage("Invalid PaymentMethod Id");
        RuleFor(r => r.Payload.Currency).NotEmpty().NotNull().WithMessage("Invalid currency");
        RuleFor(r => r.Payload.Amount).GreaterThan(0).WithMessage("Amount should be greeter then 0");
        RuleFor(r => r.Payload.InvoiceNumber).NotEmpty().NotNull().WithMessage("Invalid InvoiceNumber");
        RuleFor(r => r.Payload.ClientRedirectUrl).Matches(ValidRegex.HttpUrl)
            .WithMessage("Invalid ClientRedirectUrl");
        RuleFor(r => r.Payload.Mobile).Matches(ValidRegex.Mobile).WithMessage("Invalid Mobile");
        RuleFor(r => r.Payload.Email).Matches(ValidRegex.Email).WithMessage("Invalid Mobile");
        RuleFor(r => r.Payload.Name).NotNull().NotEmpty().WithMessage("Invalid Mobile");
        RuleFor(r => r.Payload.Items).NotNull().NotEmpty().WithMessage("Invalid Items");
    }
}