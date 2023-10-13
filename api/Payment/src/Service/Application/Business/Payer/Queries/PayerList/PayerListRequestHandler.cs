using Application.Business.Payer.Dto;
using Application.Repositories.Generic.Read;
using AutoMapper;
using ErrorHandling;
using MediatR;
using MoreLinq;

namespace Application.Business.Payer.Queries.PayerList;

public class PayerListRequestHandler : IRequestHandler<PayerListRequest, Response<PayerListDto>>
{
    private readonly IMapper _mapper;
    private readonly IReadRepository<PayerEntity> _payerReadRepository;

    public PayerListRequestHandler(IReadRepository<PayerEntity> payerReadRepository, IMapper mapper)
    {
        _payerReadRepository = payerReadRepository;
        _mapper = mapper;
    }

    public async Task<Response<PayerListDto>> Handle(PayerListRequest request, CancellationToken cancellationToken)
    {
        var payers = await _payerReadRepository.GetAll(page: request.Page, perPage: request.PerPage,
            orderBy: p => p.CreatedDateTime, orderbyDirection: OrderByDirection.Descending);

        var result = _mapper.Map<IEnumerable<PayerDto>>(payers);

        return new PayerListDto(result);
    }
}