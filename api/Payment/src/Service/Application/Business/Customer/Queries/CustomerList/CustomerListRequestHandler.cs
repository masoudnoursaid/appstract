using Application.Business.Customer.Dto;
using Application.Repositories.Generic.Read;
using AutoMapper;
using ErrorHandling;
using MediatR;
using MoreLinq;

namespace Application.Business.Customer.Queries.CustomerList;

public class CustomerListRequestHandler : IRequestHandler<CustomerListRequest, Response<CustomerListDto>>
{
    private readonly IReadRepository<CustomerEntity> _customerReadRepository;
    private readonly IMapper _mapper;

    public CustomerListRequestHandler(IReadRepository<CustomerEntity> customerReadRepository, IMapper mapper)
    {
        _customerReadRepository = customerReadRepository;
        _mapper = mapper;
    }


    public async Task<Response<CustomerListDto>> Handle(CustomerListRequest request,
        CancellationToken cancellationToken)
    {
        var customers = await _customerReadRepository.GetAll(page: request.Page, perPage: request.PerPage,
            orderBy: c => c.CreatedDateTime, orderbyDirection: OrderByDirection.Descending);

        var result = _mapper.Map<IEnumerable<CustomerDto>>(customers);

        return new CustomerListDto(result);
    }
}