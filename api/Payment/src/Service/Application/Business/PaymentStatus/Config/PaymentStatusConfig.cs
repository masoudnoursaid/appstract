using Application.Business.PaymentStatus.Dto;
using Application.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Business.PaymentStatus.Config;

public class PaymentStatusConfig : IModuleConfiguration
{
    public IServiceCollection RegisterConfiguration(IServiceCollection services)
    {
        services.AddAutoMapper(cnf => { cnf.CreateMap<PaymentStatusDto, PaymentStatusEntity>().ReverseMap(); });

        return services;
    }
}