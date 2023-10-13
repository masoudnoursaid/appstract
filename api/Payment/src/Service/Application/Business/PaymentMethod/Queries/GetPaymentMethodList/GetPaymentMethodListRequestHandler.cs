using Application.Business.PaymentMethod.Dto;
using Application.Repositories.Generic.Read;
using AutoMapper;
using ErrorHandling;
using MediatR;

namespace Application.Business.PaymentMethod.Queries.GetPaymentMethodList;

public class
    GetPaymentMethodListRequestHandler : IRequestHandler<GetPaymentMethodListRequest, Response<PaymentMethodList>>
{
    private readonly IMapper _mapper;
    private readonly IReadRepository<PaymentMethodEntity> _readRepository;

    public GetPaymentMethodListRequestHandler(IReadRepository<PaymentMethodEntity> readRepository, IMapper mapper)
    {
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<Response<PaymentMethodList>> Handle(GetPaymentMethodListRequest request,
        CancellationToken cancellationToken)
    {
        var entities =
            await _readRepository.GetAll(false, request.Page, request.PerPage);

        var dtos = _mapper.Map<IEnumerable<PaymentMethodDto>>(entities);

        return new PaymentMethodList(dtos);
    }
}