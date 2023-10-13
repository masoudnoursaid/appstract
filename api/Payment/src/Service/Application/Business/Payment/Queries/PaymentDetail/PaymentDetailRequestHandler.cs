using Application.Repositories.Generic.Read;
using AutoMapper;
using ErrorHandling;
using MediatR;

namespace Application.Business.Payment.Queries.PaymentDetail;

public class PaymentDetailRequestHandler : IRequestHandler<PaymentDetailRequest, Response<PaymentDetailDto>>
{
    private readonly IMapper _mapper;
    private readonly IReadRepository<PaymentEntity> _paymentReadRepository;

    public PaymentDetailRequestHandler(IReadRepository<PaymentEntity> paymentReadRepository, IMapper mapper)
    {
        _paymentReadRepository = paymentReadRepository;
        _mapper = mapper;
    }


    public async Task<Response<PaymentDetailDto>> Handle(PaymentDetailRequest request,
        CancellationToken cancellationToken)
    {
        PaymentEntity payment = default!;

        if (request.IncludeAllDependencies)
            payment = await _paymentReadRepository.Get(request.PaymentId,
                request.IncludeAllDependencies);

        if (!string.IsNullOrEmpty(request.NavigationPropertyPath))
            payment = await _paymentReadRepository.Get(request.PaymentId,
                request.NavigationPropertyPath!);

        var result = _mapper.Map<PaymentDetailDto>(payment);

        return result;
    }
}