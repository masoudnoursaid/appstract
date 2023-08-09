using FluentValidation;

namespace Application.Account.Queries.GetInfo;

public class GetInfoValidation : AbstractValidator<GetInfoRequest>
{
    public GetInfoValidation()
    {
        RuleFor(x => x.BirthYear).GreaterThan(1990);
    }
}