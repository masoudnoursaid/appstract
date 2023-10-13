using Application.Common.ResponseModel;
using Application.Repositories.Generic;
using Application.Repositories.Generic.Create;
using Application.Repositories.Generic.Read;
using Application.Services.Payment.BillPlz;
using Application.Services.Payment.BillPlz.Payloads;
using Application.Services.Payment.BillPlz.ResponseModel;
using Application.Services.Payment.Mollie;
using Application.Services.Payment.Mollie.Payloads;
using Application.Services.Payment.Mollie.ResponseModel;
using Application.Services.Payment.Paypal;
using Application.Services.Payment.Paypal.Payloads;
using Application.Services.Payment.Paypal.ResponseModel;
using Application.Services.Payment.Stripe;
using Application.Services.Payment.Stripe.Payloads;
using Application.Services.Payment.Stripe.ResponseModel;
using AutoMapper;
using Domain.Enums;
using Domain.Exceptions.PaymentMethod;
using ErrorHandling;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Business.Payment.Commands.VerifyPayment;

public class VerifyPaymentRequestHandler : IRequestHandler<VerifyPaymentRequest, Response<VerifyPaymentDto>>
{
    private readonly IBillPlzClientService _billPlzClientService;
    private readonly ILogger<VerifyPaymentRequestHandler> _logger;
    private readonly IMapper _mapper;
    private readonly ICreateRepository<PayerEntity> _payerCreateRepository;
    private readonly IEntityRepository<PaymentEntity> _paymentRepository;
    private readonly IEntityRepository<PaymentStatusEntity> _paymentStatusRepository;
    private readonly IPaypalClientService _paypalClientService;
    private readonly IStripeClientService _stripeClientService;
    private readonly IMollieClientService _mollieClientService;

    public VerifyPaymentRequestHandler(IEntityRepository<PaymentEntity> paymentRepository
        , IReadRepository<PaymentMethodEntity> paymentMethodRepository
        , ICreateRepository<PayerEntity> payerCreateRepository
        , IEntityRepository<PaymentStatusEntity> paymentStatusRepository
        , IPaypalClientService paypalClientService
        , IStripeClientService stripeClientService
        , IBillPlzClientService billPlzClientService
        , IMollieClientService mollieClientService
        , ILogger<VerifyPaymentRequestHandler> logger
        , IMapper mapper)
    {
        _paymentRepository = paymentRepository;
        _payerCreateRepository = payerCreateRepository;
        _paymentStatusRepository = paymentStatusRepository;
        _paypalClientService = paypalClientService;
        _billPlzClientService = billPlzClientService;
        _stripeClientService = stripeClientService;
        _mollieClientService = mollieClientService;
        _logger = logger;
        _mapper = mapper;
    }


    public async Task<Response<VerifyPaymentDto>> Handle(VerifyPaymentRequest request,
        CancellationToken cancellationToken)
    {
        var payment = await _paymentRepository.Get(request.paymentId
            , $"{nameof(PaymentEntity.PaymentMethod)};{nameof(PaymentEntity.Transaction)};{nameof(PaymentEntity.Currency)}"
            , exceptionRaiseIfNotExist: false);

        if (payment == null)
            return VerifyPaymentErrorCodes.PaymentNotFound;

        var response =
            await GetWaySwitch(request.ProvidedPaymentId ?? payment.ProvidedId!,
                payment.PaymentMethod!.GetWay);

        var payer = _mapper.Map<PayerEntity>(response);
        payer.PaymentId = payment.Id;
        await _payerCreateRepository.Insert(payer);

        if (response.Verified)
        {
            payment.Transaction!.Successful = !payment.Transaction.Successful;
            payment.StatusId = await VerifiedPaymentProcessId();
        }
        else
        {
            payment.StatusId = await RejectedPaymentProcessId();
        }

        payment.CompletedDate = DateTime.UtcNow;

        await _paymentRepository.Update(payment);

        _logger.LogInformation(
            "VERIFY_PAYMENT_REQUEST_HANDLER.HANDLER --- Payment Id : {paymentId} --- Status : {status}", payment.Id,
            payment.Transaction!.Successful);

        return new VerifyPaymentDto(payment.Id
            , payment.ProvidedId!
            , payment.Status.Title
            , payment.Transaction!.InvoiceNumber
            , payment.PaymentMethod.Title
            , payment.Currency.Title
            , payment.Amount);
    }

    private async Task<string> VerifiedPaymentProcessId()
    {
        return (await _paymentStatusRepository
            .Get(ps => ps.ProcessStatus == PaymentProcessType.PaymentSucceed)).Id;
    }

    private async Task<string> RejectedPaymentProcessId()
    {
        return (await _paymentStatusRepository
            .Get(ps => ps.ProcessStatus == PaymentProcessType.PaymentRejected)).Id;
    }


    private async Task<VerifyPaypalPaymentResponseModel> VerifyWithPaypal(VerifyPaypalPaymentPayload payload)
    {
        var result = await _paypalClientService.VerifyPayment(payload);
        return result;
    }

    private async Task<VerifyBillPlzPaymentResponseModel> VerifyWithBillPlz(VerifyBillPlzPaymentPayload payload)
    {
        var result = await _billPlzClientService.VerifyPayment(payload);
        return result;
    }

    private async Task<VerifyStripePaymentResponseModel> VerifyWithStripe(VerifyStripePaymentPayload payload)
    {
        var result = await _stripeClientService.VerifyPayment(payload);
        return result;
    }

    private async Task<VerifyMolliePaymentResponseModel> VerifyWithMollie(VerifyMolliePaymentPayload payload)
    {
        var result = await _mollieClientService.VerifyPayment(payload);
        return result;
    }

    private async Task<VerifyPaymentResponseModel> GetWaySwitch(string providedPaymentId,
        SourceImplementedGetWay getWay)
    {
        switch (getWay)
        {
            case SourceImplementedGetWay.Paypal:
            {
                var result = await VerifyWithPaypal(new VerifyPaypalPaymentPayload(providedPaymentId));
                return result;
            }
            case SourceImplementedGetWay.Billplz:
            {
                var result = await VerifyWithBillPlz(new VerifyBillPlzPaymentPayload(providedPaymentId));
                return result;
            }
            case SourceImplementedGetWay.Stripe:
            {
                var result = await VerifyWithStripe(new VerifyStripePaymentPayload(providedPaymentId));
                return result;
            }
            case SourceImplementedGetWay.Mollie:
            {
                var result = await VerifyWithMollie(new VerifyMolliePaymentPayload(providedPaymentId));
                return result;
            }
            default: throw new PaymentGetWayNotSupportedException(getWay);
        }
    }
}