using Application.Business.Payment.Dto;
using Application.Repositories.Generic.Read;
using AutoMapper;
using ErrorHandling;
using MediatR;
using MoreLinq;

namespace Application.Business.Payment.Queries.PaymentList;

public class PaymentListRequestHandler : IRequestHandler<PaymentListRequest, Response<PaymentListDto>>
{
    private readonly IMapper _mapper;
    private readonly IReadRepository<PaymentEntity> _paymentReadRepository;

    public PaymentListRequestHandler(IReadRepository<PaymentEntity> paymentReadRepository, IMapper mapper)
    {
        _paymentReadRepository = paymentReadRepository;
        _mapper = mapper;
    }


    public async Task<Response<PaymentListDto>> Handle(PaymentListRequest request, CancellationToken cancellationToken)
    {
        var payments = await _paymentReadRepository.GetAll(
            $"{nameof(PaymentEntity.PaymentMethod)};{nameof(PaymentEntity.Currency)};{nameof(PaymentEntity.Status)}",
            request.Page, request.PerPage, p => p.CreatedDateTime,
            OrderByDirection.Descending);

        var result = _mapper.Map<IEnumerable<PaymentDto>>(payments);

        return new PaymentListDto(result);
    }
}