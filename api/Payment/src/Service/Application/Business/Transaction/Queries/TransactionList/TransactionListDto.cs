using Application.Business.Transaction.Dto;

namespace Application.Business.Transaction.Queries.TransactionList;

public record TransactionListDto(IEnumerable<TransactionDto> Dtos);