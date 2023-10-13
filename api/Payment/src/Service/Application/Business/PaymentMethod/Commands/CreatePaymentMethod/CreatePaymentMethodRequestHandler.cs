using Application.Repositories.Generic.Create;
using AutoMapper;
using ErrorHandling;
using MediatR;

namespace Application.Business.PaymentMethod.Commands.CreatePaymentMethod;

public class CreatePaymentRequestHandler : IRequestHandler<CreatePaymentMethodRequest, Response>
{
    private readonly ICreateRepository<PaymentMethodEntity> _createRepository;
    private readonly IMapper _mapper;

    public CreatePaymentRequestHandler(IMapper mapper, ICreateRepository<PaymentMethodEntity> createRepository)
    {
        _mapper = mapper;
        _createRepository = createRepository;
    }


    public async Task<Response> Handle(CreatePaymentMethodRequest request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<PaymentMethodEntity>(request.Dto);
        await _createRepository.Insert(entity);

        return Response.Successful();
    }
}