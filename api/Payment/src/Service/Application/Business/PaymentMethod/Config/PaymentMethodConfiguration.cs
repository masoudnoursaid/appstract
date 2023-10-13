using Application.Business.PaymentMethod.Dto;
using Application.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Business.PaymentMethod.Config;

public class PaymentMethodConfiguration : IModuleConfiguration
{
    public IServiceCollection RegisterConfiguration(IServiceCollection services)
    {
        services.AddAutoMapper(cnf =>
        {
            cnf.CreateMap<PaymentMethodEntity, PaymentMethodDto>()
                .ReverseMap();

            cnf.CreateMap<PaymentMethodEntity, PaymentMethodModelDto>()
                .ReverseMap();
        });

        return services;
    }
}