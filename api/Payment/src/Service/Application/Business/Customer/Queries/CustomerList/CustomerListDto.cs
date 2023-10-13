using Application.Business.Customer.Dto;

namespace Application.Business.Customer.Queries.CustomerList;

public record CustomerListDto(IEnumerable<CustomerDto> Dtos);