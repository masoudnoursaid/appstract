using Application.Repositories.Generic;
using Application.Repositories.Generic.Read;
using Domain.Enums;
using ErrorHandling;
using MediatR;

namespace Application.Business.Payment.Commands.CancelPayment;

public class CancelPaymentRequestHandler : IRequestHandler<CancelPaymentRequest, Response>
{
    private readonly IEntityRepository<PaymentEntity> _paymentRepository;
    private readonly IReadRepository<PaymentStatusEntity> _paymentStatusReadRepository;

    public CancelPaymentRequestHandler(IEntityRepository<PaymentEntity> paymentRepository,
        IReadRepository<PaymentStatusEntity> paymentStatusReadRepository)
    {
        _paymentRepository = paymentRepository;
        _paymentStatusReadRepository = paymentStatusReadRepository;
    }


    public async Task<Response> Handle(CancelPaymentRequest request, CancellationToken cancellationToken)
    {
        var payment = await _paymentRepository.Get(request.PaymentId, exceptionRaiseIfNotExist: false);
        if (payment == null)
            return CancelPaymentErrorCodes.PaymentNotFound;

        var canceledStatus =
            await _paymentStatusReadRepository.Get(ps => ps.ProcessStatus == PaymentProcessType.PaymentCanceled);

        payment.StatusId = canceledStatus.Id;
        payment.CompletedDate = DateTime.UtcNow;
        await _paymentRepository.Update(payment);

        return Response.Successful();
    }
}