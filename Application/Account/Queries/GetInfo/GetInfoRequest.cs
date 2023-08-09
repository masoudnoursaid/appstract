using ErrorHandling;
using ErrorHandling.Attributes;
using ErrorHandling.Enums;
using MediatR;

namespace Application.Account.Queries.GetInfo;

[HandlerCode(HandlerCode.GetInfo)]
public record GetInfoRequest(int BirthYear) : IRequest<Response<GetInfoDto>>;