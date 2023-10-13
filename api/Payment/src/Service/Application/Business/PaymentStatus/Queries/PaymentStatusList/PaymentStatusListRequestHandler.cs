using Application.Business.PaymentStatus.Dto;
using Application.Repositories.Generic.Read;
using AutoMapper;
using ErrorHandling;
using MediatR;
using MoreLinq;

namespace Application.Business.PaymentStatus.Queries.PaymentStatusList;

public class PaymentStatusListRequestHandler : IRequestHandler<PaymentStatusListRequest, Response<PaymentStatusListDto>>
{
    private readonly IMapper _mapper;
    private readonly IReadRepository<PaymentStatusEntity> _paymentStatusEntityRepository;

    public PaymentStatusListRequestHandler(IReadRepository<PaymentStatusEntity> paymentStatusEntityRepository,
        IMapper mapper)
    {
        _paymentStatusEntityRepository = paymentStatusEntityRepository;
        _mapper = mapper;
    }


    public async Task<Response<PaymentStatusListDto>> Handle(PaymentStatusListRequest request,
        CancellationToken cancellationToken)
    {
        var statusList = await _paymentStatusEntityRepository.GetAll(page: request.Page, perPage: request.PerPage,
            orderBy: s => s.CreatedDateTime, orderbyDirection: OrderByDirection.Descending);

        var result = _mapper.Map<IEnumerable<PaymentStatusDto>>(statusList);

        return new PaymentStatusListDto(result);
    }
}