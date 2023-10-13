using Application.Business.Transaction.Dto;
using Application.Repositories.Generic.Read;
using AutoMapper;
using ErrorHandling;
using MediatR;
using MoreLinq;

namespace Application.Business.Transaction.Queries.TransactionList;

public class TransactionListRequestHandler : IRequestHandler<TransactionListRequest, Response<TransactionListDto>>
{
    private readonly IMapper _mapper;
    private readonly IReadRepository<TransactionEntity> _transactionReadRepository;

    public TransactionListRequestHandler(IReadRepository<TransactionEntity> transactionReadRepository, IMapper mapper)
    {
        _transactionReadRepository = transactionReadRepository;
        _mapper = mapper;
    }


    public async Task<Response<TransactionListDto>> Handle(TransactionListRequest request,
        CancellationToken cancellationToken)
    {
        var transactions = await _transactionReadRepository.GetAll(page: request.Page, perPage: request.PerPage,
            orderBy: t => t.CreatedDateTime, orderbyDirection: OrderByDirection.Descending);

        var result = _mapper.Map<IEnumerable<TransactionDto>>(transactions);

        return new TransactionListDto(result);
    }
}