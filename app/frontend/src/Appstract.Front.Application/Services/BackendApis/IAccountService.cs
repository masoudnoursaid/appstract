using Appstract.Front.Domain.Models.Account;
using Appstract.Front.Domain.Models.ApiResponseModels;

namespace Appstract.Front.Application.Services.BackendApis;

public interface IAccountService
{
    Task<ApiResponseBase<AccountInfoResponse>> GetInfoAsync(int? birthYear, CancellationToken cancellationToken = default);
}