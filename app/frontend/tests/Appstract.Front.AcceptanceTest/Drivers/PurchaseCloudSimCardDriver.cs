using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using Appstract.AcceptanceTest.Common.Dto;
using Appstract.AcceptanceTest.Common.Dto.CloudSimCard;
using Appstract.AcceptanceTest.Contexts;
using Appstract.Front.Application.Common.Resources;
using Appstract.Front.Domain.Models;
using Appstract.Front.Domain.Models.ApiResponseModels;
using Appstract.Front.Domain.Models.CloudSimCard;
using Appstract.Front.SharedUI.Components;
using Appstract.TestCommon.Base;
using Appstract.TestCommon.Viewers;
using Bunit;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;

namespace Appstract.AcceptanceTest.Drivers;

public class PurchaseCloudSimCardDriver : ComponentTestContext
{
    private readonly PurchaseCloudSimCardBackgroundContext _context;
    private readonly ManualResetEventSlim _pauseController;
    public IRenderedComponent<CloudSimCardPurchaseViewer> Cut { get; private set; } = null!;

    public PurchaseCloudSimCardDriver(PurchaseCloudSimCardBackgroundContext context)
    {
        _context = context;
        _pauseController = new ManualResetEventSlim(false);
        Services.AddScoped<IConfiguration, ConfigurationManager>();
    }

    public void RenderComponent()
    {
        Cut = RenderComponent<CloudSimCardPurchaseViewer>(p => p
            .Add(a => a.GetCloudSimCardCountryCarriers, GetCloudSimCardCountryCarriers)
            .Add(a => a.GetCloudSimCardMobileNumbers, GetCloudSimCardMobileNumbers)
            .Add(a => a.GetCloudSimCardAvailableCarriers, GetCloudSimCardAvailableCarriers)
            .Add(a => a.GetCloudSimCardPortPriceInfo, GetCloudSimCardPortPriceInfo)
            .Add(a => a.GetUserWalletInfo, GetUserWalletInfo)
        );
    }

    private async Task<ApiResponseBase<PaginatedResponse<CloudSimCardCountryCarrierModel>>>
        GetCloudSimCardCountryCarriers(CloudSimCardCountryCarrierRequestFilter filter)
    {
        _context.IsDelegateToGetCountryAndCarriersCalled = true;
        await Task.Run(() => _pauseController.Wait());
        ApiResponseBase<PaginatedResponse<CloudSimCardCountryCarrierModel>> response = new()
        {
            Success = true,
            Data = new PaginatedResponse<CloudSimCardCountryCarrierModel>
            {
                Items = _context.GetCloudSimCardCountryCarriersDelegateResponse,
                TotalCount = _context.GetCloudSimCardCountryCarriersDelegateResponse.Count
            }
        };
        return await Task.FromResult(response);
    }

    private async Task<ApiResponseBase<PaginatedResponse<CloudSimCardMobileNumberModel>>> GetCloudSimCardMobileNumbers(
        CloudSimCardMobileNumberRequestFilter filter)
    {
        _context.IsDelegateToGetPhoneNumbersCalled = true;
        await Task.Run(() => _pauseController.Wait());
        ApiResponseBase<PaginatedResponse<CloudSimCardMobileNumberModel>> response = new()
        {
            Success = true,
            Data = new PaginatedResponse<CloudSimCardMobileNumberModel>
            {
                Items = _context.GetCloudSimCardMobileNumbersDelegateResponse,
                TotalCount = _context.GetCloudSimCardMobileNumbersDelegateResponse.Count
            }
        };
        return await Task.FromResult(response);
    }

    private async Task<ApiResponseBase<List<CloudSimCardAvailableCarriersModel>>> GetCloudSimCardAvailableCarriers(
        List<string> countryCodes)
    {
        ApiResponseBase<List<CloudSimCardAvailableCarriersModel>> response = new()
        {
            Success = true,
            Data = _context.GetCloudSimCardCountryCarriersDelegateResponse
                .Select(x => new CloudSimCardAvailableCarriersModel { Title = x.Carrier })
                .ToList()
        };
        return await Task.FromResult(response);
    }

    private async Task<ApiResponseBase<CloudSimCardPortPriceInfoModel>> GetCloudSimCardPortPriceInfo()
    {
        _context.IsDelegateToGetCloudSimCardPortPriceInfoCalled = true;
        await Task.Run(() => _pauseController.Wait());
        return await Task.FromResult(new ApiResponseBase<CloudSimCardPortPriceInfoModel>
        {
            Success = true, Data = _context.GetCloudSimCardPortPriceInfoDelegateResponse
        });
    }

    private async Task<ApiResponseBase<UserWalletInfo>> GetUserWalletInfo()
    {
        _context.IsDelegateToGetUserWalletInfoCalled = true;
        await Task.Run(() => _pauseController.Wait());
        return await Task.FromResult(new ApiResponseBase<UserWalletInfo>
        {
            Success = true, Data = _context.GetUserWalletInfoDelegateResponse
        });
    }

    public void ContinueCountryCarriersDelegateExecution()
    {
        _pauseController.Set();
        Cut.WaitForState(() => !IsCountryAndCarriersTableLoadingVisible());
        _pauseController.Reset();
    }

    public void ContinueMobileNumbersDelegateExecution()
    {
        _pauseController.Set();
        Cut.WaitForState(() => !IsPhoneNumbersTableLoadingVisible());
        _pauseController.Reset();
    }

    public void ContinuePortPriceInfoDelegateExecution()
    {
        _pauseController.Set();
        _pauseController.Reset();
    }

    public void ContinueGetUserWalletInfoExecution()
    {
        _pauseController.Set();
        Cut.WaitForState(IsCloudSimCardPaymentPageLoaded);
        _pauseController.Reset();
    }

    public bool IsCountryAndCarriersTableLoadingVisible()
    {
        return Cut.FindComponent<MudTable<CloudSimCardCountryCarrierModel>>().Instance.Loading;
    }

    public bool IsPhoneNumbersTableLoadingVisible()
    {
        return Cut.FindComponent<MudTable<CloudSimCardMobileNumberModel>>().Instance.Loading;
    }

    public bool IsCloudSimCardPaymentPageLoaded()
    {
        return Cut.FindComponents<LoadingWrapper>()
            .First(c => c.Instance.Class == "cloud-sim-card__payment-loading")
            .Instance.IsLoaded;
    }

    public List<ProgressStepsIndicatorDto> GetProgressStepsIndicator()
    {
        return Cut.FindAll(".progress-indicator .progress-indicator__step").Select(x => new ProgressStepsIndicatorDto
        {
            Title = x.QuerySelector("p")?.Text() ?? "",
            Status = x.ClassList.Any(c => c.Contains("active"))
                ? x.QuerySelectorAll("svg").Any() ? "Completed" : "Current"
                : "Upcoming"
        }).ToList();
    }

    public List<CloudSimCardCountryCarrierDto> GetCountryCarrierTableResponse()
    {
        Cut.WaitForAssertion(() =>
        {
            IsCountryAndCarriersTableLoadingVisible().Should().BeFalse();
        });

        IRefreshableElementCollection<IElement> rows = Cut.FindAll(".cloud-sim-card__country-carrier tbody tr");
        List<CloudSimCardCountryCarrierDto> result = new();

        foreach (IElement tr in rows)
        {
            Dictionary<string, IElement> dic =
                tr.Children.Where(td => td.HasAttribute("data-label"))
                    .ToDictionary(td => td.GetAttribute("data-label")!, td => td);

            result.Add(new CloudSimCardCountryCarrierDto
            {
                Country = dic[CloudSimCardResource.Country].Text().Trim(),
                Carrier = dic[CloudSimCardResource.Carrier].Text().Trim(),
                MonthlyFee = dic[CloudSimCardResource.MonthlyFee].Text().Trim(),
                DomesticSmsFee = dic[CloudSimCardResource.DomesticSMSFee].Text().Trim(),
                DomesticCallFee = dic[CloudSimCardResource.DomesticCallFee].Text().Trim(),
                NumberRange = dic[CloudSimCardResource.NumberRange].Text().Trim(),
            });
        }

        return result;
    }

    public List<CloudSimCardMobileNumberDto> GetPhoneNumbersTableResponse()
    {
        Cut.WaitForAssertion(() =>
        {
            IsPhoneNumbersTableLoadingVisible().Should().BeFalse();
        });

        IRefreshableElementCollection<IElement> rows = Cut.FindAll(".cloud-sim-card__mobile-numbers tbody tr");
        List<CloudSimCardMobileNumberDto> result = new();

        foreach (IElement tr in rows)
        {
            Dictionary<string, IElement> dic =
                tr.Children.Where(td => td.HasAttribute("data-label"))
                    .ToDictionary(td => td.GetAttribute("data-label")!, td => td);

            result.Add(new CloudSimCardMobileNumberDto
            {
                Country = dic[CloudSimCardResource.Country].Text().Trim(),
                Carrier = dic[CloudSimCardResource.Carrier].Text().Trim(),
                MonthlyFee = dic[CloudSimCardResource.MonthlyFee].Text().Trim(),
                DomesticSmsFee = dic[CloudSimCardResource.DomesticSMSFee].Text().Trim(),
                DomesticCallFee = dic[CloudSimCardResource.DomesticCallFee].Text().Trim(),
                MobileNumber = dic[CloudSimCardResource.MobileNumber].Text().Trim(),
                SimSetupFee = dic[CloudSimCardResource.SimSetupFee].Text().Trim(),
            });
        }

        return result;
    }

    public List<CloudSimCardPriceInfoDto> GetPriceInfoTableResponse()
    {
        IRefreshableElementCollection<IElement> items = Cut.FindAll(".cloud-sim-card__detail-paper");

        return items.Select(x => new CloudSimCardPriceInfoDto
        {
            MobileNumber = x.QuerySelector(".cloud-sim-card__detail-number")!.Text().Trim(),
            PortType = x.QuerySelector(".cloud-sim-card__detail-port")!.Text().Trim(),
            PriceInfo = string.Join(' ',
                x.QuerySelectorAll(".cloud-sim-card__detail-list p").Select(p => p.Text()).ToList()),
        }).ToList();
    }
}