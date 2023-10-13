using Application.Repositories.Generic.Create;
using AutoMapper;
using ErrorHandling;
using MediatR;

namespace Application.Business.Currency.Commands.CreateCurrency;

public class CreateCurrencyRequestHandler : IRequestHandler<CreateCurrencyRequest, Response>
{
    private readonly ICreateRepository<CurrencyEntity> _createRepository;
    private readonly IMapper _mapper;

    public CreateCurrencyRequestHandler(ICreateRepository<CurrencyEntity> createRepository, IMapper mapper)
    {
        _createRepository = createRepository;
        _mapper = mapper;
    }

    public async Task<Response> Handle(CreateCurrencyRequest request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CurrencyEntity>(request.Dto);
        await _createRepository.Insert(entity);
        return Response.Successful();
    }
}