using Application.Common.BaseTypes.Dto;

namespace Application.Business.Applications.Command.RegisterApplication;

public record RegisterApplicationResultDto(string ApikeyValue, string ApiSecretValue) : IBaseDto;