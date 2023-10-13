using Application.Repositories.Generic.Create;
using Application.Services.IP;
using AutoMapper;
using Domain.ValueObjects;
using ErrorHandling;
using MediatR;

namespace Application.Business.Applications.Command.RegisterApplication;

public class
    RegisterApplicationRequestHandler : IRequestHandler<RegisterApplicationRequest,
        Response<RegisterApplicationResultDto>>
{
    private readonly ICreateRepository<ApplicationEntity> _createRepository;
    private readonly IIpService _ipService;
    private readonly IMapper _mapper;

    public RegisterApplicationRequestHandler(ICreateRepository<ApplicationEntity> createRepository
        , IIpService ipService
        , IMapper mapper)
    {
        _createRepository = createRepository;
        _ipService = ipService;
        _mapper = mapper;
    }

    public async Task<Response<RegisterApplicationResultDto>> Handle(RegisterApplicationRequest request,
        CancellationToken cancellationToken)
    {
        var apiKey = new ApiKey(_ipService.GetRegion());
        var apiSecret = new ApiSecret();
        var data = _mapper.Map<ApplicationEntity>(request);
        data.SetApiKey(apiKey);
        data.SetApiSecret(apiSecret);

        await _createRepository.Insert(data);
        return new RegisterApplicationResultDto(apiKey.Value, apiSecret.Value);
    }
}