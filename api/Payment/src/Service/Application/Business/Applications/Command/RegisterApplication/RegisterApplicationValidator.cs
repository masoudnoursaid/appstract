using Application.Common.Regex;
using FluentValidation;

namespace Application.Business.Applications.Command.RegisterApplication;

public class RegisterApplicationValidator : AbstractValidator<RegisterApplicationRequest>
{
    public RegisterApplicationValidator()
    {
        RuleFor(app => app.Title).NotEmpty().NotNull().WithMessage("Invalid Title");
        RuleForEach(app => app.AuthorizedIpAddresses).NotEmpty().NotNull()
            .Matches(ValidIpRegex.Ip)
            .WithMessage("Invalid Authorized IP(s)");
    }
}