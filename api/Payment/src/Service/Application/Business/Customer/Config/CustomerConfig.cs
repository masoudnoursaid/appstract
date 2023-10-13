using Application.Business.Customer.Dto;
using Application.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Business.Customer.Config;

public class CustomerConfig : IModuleConfiguration
{
    public IServiceCollection RegisterConfiguration(IServiceCollection services)
    {
        services.AddAutoMapper(cnf => { cnf.CreateMap<CustomerDto, CustomerEntity>().ReverseMap(); });

        return services;
    }
}