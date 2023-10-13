using Application.Common.Utils;
using Application.Repositories.Generic.Delete;
using ErrorHandling;
using MediatR;

namespace Application.Business.PaymentMethod.Commands.DeletePaymentMethod;

public class DeletePaymentMethodRequestHandler : IRequestHandler<DeletePaymentMethodRequest, Response>
{
    private readonly IDeleteRepository<PaymentMethodEntity> _deleteRepository;

    public DeletePaymentMethodRequestHandler(IDeleteRepository<PaymentMethodEntity> deleteRepository)
    {
        _deleteRepository = deleteRepository;
    }

    public async Task<Response> Handle(DeletePaymentMethodRequest request, CancellationToken cancellationToken)
    {
        var result =
            await Try.HasException<Exception>(
                () => _deleteRepository.Delete(request.Id, true));

        return result ? DeletePaymentMethodErrorCodes.PaymentMethodNotFound : Response.Successful();
    }
}