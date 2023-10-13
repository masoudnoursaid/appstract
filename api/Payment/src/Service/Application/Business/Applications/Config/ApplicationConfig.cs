using Application.Business.Applications.Command.RegisterApplication;
using Application.Business.Applications.Dto;
using Application.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IPAddress = Domain.ValueObjects.IpAddress;

namespace Application.Business.Applications.Config;

public class ApplicationConfig : IModuleConfiguration
{
    public IServiceCollection RegisterConfiguration(IServiceCollection services)
    {
        services.AddAutoMapper(cnf =>
        {
            cnf.CreateMap<RegisterApplicationRequest, ApplicationEntity>()
                .ForMember(dest => dest.AuthorizedIpAddresses,
                    opt =>
                        opt.MapFrom(src
                            => src.AuthorizedIpAddresses.Select(IPAddress.Create)))
                .ReverseMap();

            cnf.CreateMap<ApplicationDto, ApplicationEntity>()
                .ForPath(dest => dest.ApiKey!.Value,
                    opt =>
                        opt.MapFrom(src => src.ApiKey))
                .ForPath(dest => dest.ApiSecret!.Value,
                    opt =>
                        opt.MapFrom(src => src.ApiSecret))
                .ReverseMap();
        });

        return services;
    }
}