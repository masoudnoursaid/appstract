using Appstract.Front.Application.Services;
using Appstract.Front.Infrastructure.Services;
using Appstract.Front.Infrastructure.Services.DateTimeService;
using Appstract.Front.InfrastructureMock.FakeServices;
using Appstract.TestCommon.Base;
using Blazored.LocalStorage;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;

namespace Appstract.TestCommon.Extensions;

public static class TestContextExtensions
{
    public static TestContext RegisterDependencies(this TestContext context, 
        ISyncLocalStorageService localStorage,
        IDateTimeService dateTimeService)
    {
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        context.Services.AddScoped<IErrorMessageService, ErrorMessageService>();
        context.Services.AddScoped<IProfileService,FakeProfileService>();
        context.Services.AddScoped<ILogger, TestLogger>();
        context.Services.AddBlazoredLocalStorage();
        context.Services.AddSingleton(localStorage);
        context.Services.AddMudServices();
        context.Services.AddScoped<HttpClientService>();
        context.Services.AddSingleton(dateTimeService);
        return context;
    }
}