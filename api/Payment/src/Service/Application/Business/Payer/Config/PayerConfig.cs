using Application.Business.Payer.Dto;
using Application.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Business.Payer.Config;

public class PayerConfig : IModuleConfiguration
{
    public IServiceCollection RegisterConfiguration(IServiceCollection services)
    {
        services.AddAutoMapper(cnf => { cnf.CreateMap<PayerDto, PayerEntity>().ReverseMap(); });

        return services;
    }
}