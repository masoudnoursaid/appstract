using Application.Business.Currency.Dto;
using Application.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Business.Currency.Config;

public class CurrencyConfiguration : IModuleConfiguration
{
    public IServiceCollection RegisterConfiguration(IServiceCollection services)
    {
        services.AddAutoMapper(cnf =>
        {
            cnf.CreateMap<CurrencyEntity, CurrencyDto>()
                .ReverseMap();

            cnf.CreateMap<CurrencyEntity, CurrencyModelDto>()
                .ReverseMap();
        });

        return services;
    }
}