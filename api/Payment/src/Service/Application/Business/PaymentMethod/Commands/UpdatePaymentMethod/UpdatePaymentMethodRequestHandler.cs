using Application.Common.Utils;
using Application.Repositories.Generic.Update;
using AutoMapper;
using ErrorHandling;
using MediatR;

namespace Application.Business.PaymentMethod.Commands.UpdatePaymentMethod;

public class UpdatePaymentMethodRequestHandler : IRequestHandler<UpdatePaymentMethodRequest, Response>
{
    private readonly IMapper _mapper;
    private readonly IUpdateRepository<PaymentMethodEntity> _updateRepository;

    public UpdatePaymentMethodRequestHandler(IUpdateRepository<PaymentMethodEntity> updateRepository, IMapper mapper)
    {
        _updateRepository = updateRepository;
        _mapper = mapper;
    }

    public async Task<Response> Handle(UpdatePaymentMethodRequest request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<PaymentMethodEntity>(request.Dto);
        entity.Id = request.Id;

        var result =
            await Try.HasException<Exception>(() => _updateRepository.Update(entity, true));

        return result ? UpdatePaymentMethodErrorCodes.PaymentMethodNotFound : Response.Successful();
    }
}