using Appstract.Front.Application.Common.Resources;
using Appstract.Front.Domain.Enums;
using Appstract.Front.Domain.Models.ApiResponseModels;
using Appstract.Front.SharedUI.Components;
using Appstract.TestCommon.Base;
using FluentAssertions;
using MudBlazor;

namespace Appstract.UnitTest.Components;

public class LoadingWrapperTests : ComponentTestContext
{
    [Fact]
    public void LoadingWrapper_RenderedSuccessfully_ExistenceElements()
    {
        IRenderedComponent<LoadingWrapper> cut = RenderComponent<LoadingWrapper>();

        cut.Find(".loading-wrapper").Should().NotBeNull();
        cut.Find(".loading-wrapper").TextContent.Should().Contain(LoadingWrapperResource.PleaseWait);
        cut.FindComponent<MudProgressLinear>().Should().NotBeNull();
    }

    [Fact]
    public void LoadingWrapper_IsLoaded_ExistenceElements()
    {
        IRenderedComponent<LoadingWrapper> cut = RenderComponent<LoadingWrapper>(s =>
            s.Add(a => a.IsLoaded, true));

        cut.Find(".content-wrapper");
        cut.Markup.Should().NotBeNullOrEmpty();
    }

    [Theory]
    [InlineData(1, AlertType.Warning)]
    [InlineData(2, AlertType.Error)]
    public void LoadingWrapper_HasError_ExistenceElements(int errorType, AlertType alertType)
    {
        Error error = new() { Code = 11111111, Message = "Content has error" , Type = errorType};

        IRenderedComponent<LoadingWrapper> cut = RenderComponent<LoadingWrapper>(s =>
            s.Add(a => a.HasError, true)
                .Add(a => a.IsLoaded, true)
                .Add(a => a.Error, error));

        cut.Find(".content-wrapper");
        IRenderedComponent<MudAlert> alert = cut.FindComponent<MudAlert>();
        alert.Markup.Should().Contain(error.FormattedCode);
        error.FormattedCode.Should().Be("11_111_111");
        error.FormattedType.Should().Be(alertType);
        alert.Markup.Should().Contain(error.Message);
    }
}
