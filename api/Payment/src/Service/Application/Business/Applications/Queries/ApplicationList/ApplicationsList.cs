using Application.Business.Applications.Dto;

namespace Application.Business.Applications.Queries.ApplicationList;

public record ApplicationsList(IEnumerable<ApplicationDto> applications);