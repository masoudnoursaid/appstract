using Application.Common.Payloads;
using Application.Common.ResponseModel;
using Application.Repositories.Generic.Create;
using Application.Repositories.Generic.Read;
using Application.Services.HttpAuth;
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

namespace Application.Business.Payment.Commands.CreatePayment;

public class CreatePaymentRequestHandler : IRequestHandler<CreatePaymentRequest, Response<CreatePaymentDto>>
{
    private readonly IBillPlzClientService _billPlzClientService;
    private readonly IStripeClientService _stripeClientService;
    private readonly IPaypalClientService _paypalClientService;
    private readonly IMollieClientService _mollieClientService;
    private readonly IHttpAuthService _httpAuthService;
    private readonly IReadRepository<CurrencyEntity> _currencyReadRepository;
    private readonly IReadRepository<ApplicationEntity> _applicationReadRepository;
    private readonly ICreateRepository<CustomerEntity> _customerCreateRepository;
    private readonly ICreateRepository<PaymentEntity> _paymentCreateRepository;
    private readonly IReadRepository<PaymentMethodEntity> _paymentMethodReadRepository;
    private readonly IReadRepository<PaymentStatusEntity> _paymentStatusReadRepository;
    private readonly ICreateRepository<TransactionEntity> _transactionCreateRepository;
    private readonly ILogger<CreatePaymentRequestHandler> _logger;
    private readonly IMapper _mapper;

    public CreatePaymentRequestHandler(ICreateRepository<PaymentEntity> paymentCreateRepository
        , ICreateRepository<TransactionEntity> transactionCreateRepository
        , IReadRepository<CurrencyEntity> currencyReadRepository
        , ICreateRepository<CustomerEntity> customerCreateRepository
        , IReadRepository<PaymentStatusEntity> paymentStatusReadRepository
        , IReadRepository<PaymentMethodEntity> paymentMethodReadRepository
        , IPaypalClientService paypalClientService
        , IBillPlzClientService billPlzClientService
        , IStripeClientService stripeClientService
        , IMollieClientService mollieClientService
        , IHttpAuthService httpAuthService
        , ILogger<CreatePaymentRequestHandler> logger
        , IReadRepository<ApplicationEntity> applicationEntity
        , IReadRepository<ApplicationEntity> applicationReadRepository
        , IMapper mapper)
    {
        _paymentCreateRepository = paymentCreateRepository;
        _transactionCreateRepository = transactionCreateRepository;
        _currencyReadRepository = currencyReadRepository;
        _customerCreateRepository = customerCreateRepository;
        _paymentStatusReadRepository = paymentStatusReadRepository;
        _paymentMethodReadRepository = paymentMethodReadRepository;
        _paypalClientService = paypalClientService;
        _billPlzClientService = billPlzClientService;
        _stripeClientService = stripeClientService;
        _logger = logger;
        _mapper = mapper;
        _mollieClientService = mollieClientService;
        _httpAuthService = httpAuthService;
        _applicationReadRepository = applicationReadRepository;
    }


    public async Task<Response<CreatePaymentDto>> Handle(CreatePaymentRequest request,
        CancellationToken cancellationToken)
    {
        var apiKey = await _httpAuthService.GetApiKeyFromContext();

        var application =
            await _applicationReadRepository.Get(a => a.ApiKey!.Value == apiKey, exceptionRaiseIfNotExist: false);

        if (application == null)
            return CreatePaymentErrorCodes.ApplicationNotFound;

        var paymentMethod =
            await _paymentMethodReadRepository.Get(request.PaymentMethodId, exceptionRaiseIfNotExist: false);

        if (paymentMethod == null)
            return CreatePaymentErrorCodes.MethodNotFound;

        var currency =
            await _currencyReadRepository.Get(c =>
                    c.Title.Equals(request.Payload.Currency, StringComparison.OrdinalIgnoreCase)
                , exceptionRaiseIfNotExist: false);

        if (currency == null)
            return CreatePaymentErrorCodes.CurrencyNotFound;

        var transaction = new TransactionEntity
        {
            InvoiceNumber = request.Payload.InvoiceNumber,
            Description = request.Payload.Description,
            Items = request.Payload.Items
        };
        await _transactionCreateRepository.Insert(transaction);


        var paymentId = Guid.NewGuid().ToString();
        var response = await GetWaySwitch(request.Payload, paymentId, paymentMethod.GetWay,
            paymentMethod.Sandbox ?? false);

        var status =
            await _paymentStatusReadRepository.Get(ps => ps.ProcessStatus == PaymentProcessType.PaymentCreated);

        var payment = new PaymentEntity
        {
            Id = paymentId,
            ClientRedirectUrl = request.Payload.ClientRedirectUrl,
            ClientWebHookUrl = request.Payload.ClientWebHookUrl,
            Amount = request.Payload.Amount,
            PaymentMethodId = paymentMethod.Id,
            CurrencyId = currency.Id,
            StatusId = status.Id,
            TransactionId = transaction.Id,
            ProvidedId = response.ProvidedId,
            ApplicationId = application.Id
        };

        await _paymentCreateRepository.Insert(payment);

        var customer = new CustomerEntity
        {
            Email = request.Payload.Email,
            Mobile = request.Payload.Mobile,
            Name = request.Payload.Name,
            PaymentId = paymentId
        };

        await _customerCreateRepository.Insert(customer);

        _logger.LogInformation(
            "CREATE_PAYMENT_REQUEST.HANDLER --- Payment created with following info --- GetWay : {getWay} \n paymentUrl : {paymentUrl} \n providedId : {providedId}",
            paymentMethod.GetWay, response.Url, payment.ProvidedId);

        return new CreatePaymentDto(response.Url, response.ProvidedId, payment.Id);
    }

    private async Task<CreatePaypalPaymentResponseModel> PayWithPaypal(CreatePaypalPaymentPayload payload,
        bool useSandbox = true)
    {
        var result = await _paypalClientService.GeneratePaymentUrl(payload, useSandbox);
        return result;
    }

    private async Task<CreateBillPlzPaymentResponseModel> PayWithBillPlz(CreateBillPlzPaymentPayload payload,
        bool useSandbox = true)
    {
        var result = await _billPlzClientService.GeneratePaymentUrl(payload, useSandbox);
        return result;
    }

    private async Task<CreateStripePaymentResponseModel> PayWithStripe(CreateStripePaymentPayload payload,
        bool useSandbox = true)
    {
        var result = await _stripeClientService.GeneratePaymentUrl(payload, useSandbox);
        return result;
    }

    private async Task<CreateMolliePaymentResponseModel> PayWithMollie(CreateMolliePaymentPayload payload,
        bool useSandbox = true)
    {
        var result = await _mollieClientService.GeneratePaymentUrl(payload, useSandbox);
        return result;
    }

    private async Task<CreatePaymentResponseModel> GetWaySwitch(CreatePaymentPayload payload
        , string paymentId
        , SourceImplementedGetWay getWay
        , bool useSandbox = true)
    {
        switch (getWay)
        {
            case SourceImplementedGetWay.Paypal:
            {
                var paypalPayload = _mapper.Map<CreatePaypalPaymentPayload>(payload);
                paypalPayload.PaymentId = paymentId;
                var result = await PayWithPaypal(paypalPayload, useSandbox);
                return _mapper.Map<CreatePaymentResponseModel>(result);
            }
            case SourceImplementedGetWay.Billplz:
            {
                var billPlzPayload = _mapper.Map<CreateBillPlzPaymentPayload>(payload);
                billPlzPayload.PaymentId = paymentId;
                var result = await PayWithBillPlz(billPlzPayload, useSandbox);
                return _mapper.Map<CreatePaymentResponseModel>(result);
            }
            case SourceImplementedGetWay.Stripe:
            {
                var stripePayload = _mapper.Map<CreateStripePaymentPayload>(payload);
                stripePayload.PaymentId = paymentId;
                var result = await PayWithStripe(stripePayload, useSandbox);
                return _mapper.Map<CreatePaymentResponseModel>(result);
            }
            case SourceImplementedGetWay.Mollie:
            {
                var molliePayload = _mapper.Map<CreateMolliePaymentPayload>(payload);
                molliePayload.PaymentId = paymentId;
                var result = await PayWithMollie(molliePayload, useSandbox);
                return _mapper.Map<CreatePaymentResponseModel>(result);
            }
            default: throw new PaymentGetWayNotSupportedException(getWay);
        }
    }
}