using Application.Business.Applications.Dto;
using Application.Repositories.Generic.Read;
using AutoMapper;
using ErrorHandling;
using MediatR;

namespace Application.Business.Applications.Queries.ApplicationList;

public class ApplicationListRequestHandler : IRequestHandler<ApplicationListRequest, Response<ApplicationsList>>
{
    private readonly IReadRepository<ApplicationEntity> _applicationReadRepository;
    private readonly IMapper _mapper;

    public ApplicationListRequestHandler(IReadRepository<ApplicationEntity> applicationReadRepository,
        IMapper mapper)
    {
        _applicationReadRepository = applicationReadRepository;
        _mapper = mapper;
    }


    public async Task<Response<ApplicationsList>> Handle(ApplicationListRequest request,
        CancellationToken cancellationToken)
    {
        var entities = await _applicationReadRepository.GetAll(false, request.Page
            , request.PerPage);
        var data = _mapper.Map<IEnumerable<ApplicationDto>>(entities);
        return new ApplicationsList(data);
    }
}