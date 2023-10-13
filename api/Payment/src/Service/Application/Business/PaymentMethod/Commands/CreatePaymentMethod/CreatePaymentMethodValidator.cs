using FluentValidation;

namespace Application.Business.PaymentMethod.Commands.CreatePaymentMethod;

public class CreatePaymentMethodValidator
{
    public class CreatePassportRecordValidator : AbstractValidator<CreatePaymentMethodRequest>
    {
    }
}