using System.Globalization;
using Application.Business.Currency.Dto;
using Application.Business.Transaction.Dto;
using Application.Repositories.Generic.Read;
using AutoMapper;
using ErrorHandling;
using MediatR;

namespace Application.Business.Payment.Queries.PaymentVisualize;

public class PaymentVisualizeRequestHandler : IRequestHandler<PaymentVisualizeRequest, Response<PaymentVisualizeDto>>
{
    private readonly IReadRepository<PaymentEntity> _paymentReadRepository;
    private readonly IMapper _mapper;

    public PaymentVisualizeRequestHandler(IReadRepository<PaymentEntity> paymentReadRepository, IMapper mapper)
    {
        _paymentReadRepository = paymentReadRepository;
        _mapper = mapper;
    }

    public async Task<Response<PaymentVisualizeDto>> Handle(PaymentVisualizeRequest request,
        CancellationToken cancellationToken)
    {
        var payment = await _paymentReadRepository.Get(request.PaymentId,
            navigationPropertyPath:
            $"{nameof(PaymentEntity.Transaction)};{nameof(PaymentEntity.PaymentMethod)};{nameof(PaymentEntity.Currency)};{nameof(PaymentEntity.Status)};{nameof(PaymentEntity.Application)}");


        return new PaymentVisualizeDto(payment.Id
            , payment.ProvidedId!
            , payment.PaymentMethod!.Title
            , payment.PaymentMethod.Icon!
            , payment.Amount.ToString(CultureInfo.InvariantCulture)
            , payment.ClientRedirectUrl
            , payment.Status.Title
            , payment.CompletedDate!.Value
            , _mapper.Map<CurrencyDto>(payment.Currency)
            , _mapper.Map<TransactionDto>(payment.Transaction)
            , payment.ClientWebHookUrl
            , payment.Application.ApiSecret!.Value);
    }
}