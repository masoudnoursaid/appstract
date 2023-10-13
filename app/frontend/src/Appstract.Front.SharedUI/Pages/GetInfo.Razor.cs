using System.ComponentModel.DataAnnotations;
using Appstract.Front.Application.Common.Constants;
using Appstract.Front.Application.Services.BackendApis;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace Appstract.Front.SharedUI.Pages;

public partial class GetInfo : IDisposable
{
    private const string ROUTE = Routes.GET_INFO;
    
    [Inject]
    private ILogger<GetInfo> Logger { get; set; }

    [Inject]
    private IAccountService AccountService { get; set; } = null!;

    private readonly CancellationTokenSource _cts = new();

    public AccountGetInfoRequestModel? Model { get; set; }
    private AccountGetInfoResponseModel? Response { get; set; }

    protected override void OnInitialized()
    {
        Model ??= new AccountGetInfoRequestModel();
    }

    private async Task SubmitAsync()
    {
        Logger.LogInformation("Model.Id = {Id}", Model?.BirthYear);

        var result = await AccountService.GetInfoAsync(Model?.BirthYear, _cts.Token);
        if (!result.Success)
        {
            Logger.LogError("Failed to fetch result. {ErrorMessage}", result.Error.Message);
        }

        Logger.LogInformation("Result = {Result}", result.Data.Age);
        Response = new AccountGetInfoResponseModel { Age = result.Data.Age };
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        _cts.Cancel();
        _cts.Dispose();

        if (disposing)
        {
            // Native resource cleanup
        }
    }

    ~GetInfo()
    {
        Dispose(false);
    }
    
    public class AccountGetInfoRequestModel
    {
        [Required]
        public int? BirthYear { get; set; } = 2000;
    }

    public class AccountGetInfoResponseModel
    {
        public int Age { get; set; }
    }
}