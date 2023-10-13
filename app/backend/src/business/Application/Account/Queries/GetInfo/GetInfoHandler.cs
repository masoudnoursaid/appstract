using ErrorHandling;
using MediatR;

namespace Application.Account.Queries.GetInfo;

public class GetInfoHandler : IRequestHandler<GetInfoRequest, Response<GetInfoDto>>
{
    public Task<Response<GetInfoDto>> Handle(GetInfoRequest request, CancellationToken cancellationToken)
    {
        int age = DateTime.Now.Year - request.BirthYear;
        if (age < 20)
        {
            return Task.FromResult<Response<GetInfoDto>>(GetInfoErrorCodes.AgeIsLessThan20);
        }

        return Task.FromResult<Response<GetInfoDto>>(new GetInfoDto(age));
    }
}