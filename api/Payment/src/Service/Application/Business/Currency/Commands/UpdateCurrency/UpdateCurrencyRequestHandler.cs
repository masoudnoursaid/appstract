using Application.Common.Utils;
using Application.Repositories.Generic.Update;
using AutoMapper;
using ErrorHandling;
using MediatR;

namespace Application.Business.Currency.Commands.UpdateCurrency;

public class UpdateCurrencyRequestHandler : IRequestHandler<UpdateCurrencyRequest, Response>
{
    private readonly IMapper _mapper;
    private readonly IUpdateRepository<CurrencyEntity> _updateRepository;

    public UpdateCurrencyRequestHandler(IUpdateRepository<CurrencyEntity> updateRepository, IMapper mapper)
    {
        _updateRepository = updateRepository;
        _mapper = mapper;
    }

    public async Task<Response> Handle(UpdateCurrencyRequest request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CurrencyEntity>(request.Dto);
        entity.Id = request.Id;

        var result = await Try.HasException<Exception>(() => _updateRepository.Update(entity));

        return result ? UpdateCurrencyErrorCodes.CurrencyNotFound : Response.Successful();
    }
}