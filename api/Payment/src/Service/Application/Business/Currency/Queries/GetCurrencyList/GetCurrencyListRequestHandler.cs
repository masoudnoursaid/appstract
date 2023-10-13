using Application.Business.Currency.Dto;
using Application.Repositories.Generic.Read;
using AutoMapper;
using ErrorHandling;
using MediatR;

namespace Application.Business.Currency.Queries.GetCurrencyList;

public class GetCurrencyListRequestHandler : IRequestHandler<GetCurrencyListRequest, Response<CurrencyList>>
{
    private readonly IMapper _mapper;
    private readonly IReadRepository<CurrencyEntity> _readRepository;

    public GetCurrencyListRequestHandler(IReadRepository<CurrencyEntity> readRepository, IMapper mapper)
    {
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<Response<CurrencyList>> Handle(GetCurrencyListRequest request,
        CancellationToken cancellationToken)
    {
        var entities =
            await _readRepository.GetAll(false, request.Page, request.PerPage);

        var dtos = _mapper.Map<IEnumerable<CurrencyDto>>(entities);

        return new CurrencyList(dtos);
    }
}