using Application.Business.Payment.Dto;
using Application.Business.Payment.Queries.PaymentDetail;
using Application.Common.Payloads;
using Application.Common.ResponseModel;
using Application.Configuration;
using Application.Services.Payment.BillPlz.Payloads;
using Application.Services.Payment.BillPlz.ResponseModel;
using Application.Services.Payment.Mollie.Payloads;
using Application.Services.Payment.Mollie.ResponseModel;
using Application.Services.Payment.Paypal.Payloads;
using Application.Services.Payment.Paypal.ResponseModel;
using Application.Services.Payment.Stripe.Payloads;
using Application.Services.Payment.Stripe.ResponseModel;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Business.Payment.Config;

public class PaymentConfig : IModuleConfiguration
{
    public IServiceCollection RegisterConfiguration(IServiceCollection services)
    {
        services.AddAutoMapper(cnf =>
        {
            cnf.CreateMap<CreatePaymentPayload, CreatePaypalPaymentPayload>().ReverseMap();
            cnf.CreateMap<CreatePaymentResponseModel, CreatePaypalPaymentResponseModel>().ReverseMap();

            cnf.CreateMap<VerifyPaymentResponseModel, PayerEntity>().ReverseMap();

            cnf.CreateMap<CreatePaymentPayload, CreateBillPlzPaymentPayload>().ReverseMap();
            cnf.CreateMap<CreatePaymentResponseModel, CreateBillPlzPaymentResponseModel>().ReverseMap();
            
            cnf.CreateMap<CreatePaymentPayload, CreateMolliePaymentPayload>().ReverseMap();
            cnf.CreateMap<CreatePaymentResponseModel, CreateMolliePaymentResponseModel>().ReverseMap();

            cnf.CreateMap<CreatePaymentPayload, CreateStripePaymentPayload>().ReverseMap();
            cnf.CreateMap<CreatePaymentResponseModel, CreateStripePaymentResponseModel>().ReverseMap();

            cnf.CreateMap<PaymentEntity, PaymentDto>()
                .ForPath(d => d.PaymentMethodDisplayTitle
                    , c
                        => c.MapFrom(s => s.PaymentMethod!.DisplayTitle))
                .ReverseMap();

            cnf.CreateMap<PaymentEntity, PaymentDetailDto>().ReverseMap();
        });

        return services;
    }
}