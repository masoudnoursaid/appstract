using ErrorHandling;
using ErrorHandling.Attributes;
using ErrorHandling.Enums;
using MediatR;

namespace Application.Business.Applications.Command.RegisterApplication;

[HandlerCode(HandlerCode.RegisterApplication)]
public record RegisterApplicationRequest
    (IEnumerable<string> AuthorizedIpAddresses, string Title) : IRequest<Response<RegisterApplicationResultDto>>;