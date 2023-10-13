using Application.Business.Transaction.Dto;
using Application.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Business.Transaction.Config;

public class TransactionConfig : IModuleConfiguration
{
    public IServiceCollection RegisterConfiguration(IServiceCollection services)
    {
        services.AddAutoMapper(cnf => { cnf.CreateMap<TransactionDto, TransactionEntity>().ReverseMap(); });

        return services;
    }
}