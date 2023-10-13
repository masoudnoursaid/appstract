using Application.Common.Utils;
using Application.Repositories.Generic.Delete;
using ErrorHandling;
using MediatR;

namespace Application.Business.Currency.Commands.DeleteCurrency;

public class DeleteCurrencyRequestHandler : IRequestHandler<DeleteCurrencyRequest, Response>
{
    private readonly IDeleteRepository<CurrencyEntity> _deleteRepository;

    public DeleteCurrencyRequestHandler(IDeleteRepository<CurrencyEntity> deleteRepository)
    {
        _deleteRepository = deleteRepository;
    }

    public async Task<Response> Handle(DeleteCurrencyRequest request, CancellationToken cancellationToken)
    {
        var result =
            await Try.HasException<Exception>(
                () => _deleteRepository.Delete(request.Id, true));
        return result ? DeleteCurrencyErrorCodes.CurrencyNotFound : Response.Successful();
    }
}