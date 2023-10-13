using Appstract.Front.Domain.Enums;
using Appstract.Front.SharedUI.Components.Common;
using Appstract.TestCommon.Base;
using FluentAssertions;
using MudBlazor;

namespace Appstract.UnitTest.Components;

public class AlertMessageTest : ComponentTestContext
    {
        [Fact]
        public void AlertMessageTest_ShowIsTrueAndErrorTypeNotDeclared_ShouldShowMessageAsSuccessfulMessage()
        {
            string className = "class";
            string message = "message";
            string formattedErrorCode = "11_123_456";
            
            IRenderedComponent<AlertMessage> cut = RenderComponent<AlertMessage>(
                parameters => parameters
                    .Add(x => x.Class, className)
                    .Add(x => x.Message, message)
                    .Add(x => x.Show, true)
            );

            cut.FindComponents<MudAlert>().Count.Should().Be(1);
            cut.FindComponent<MudAlert>().Markup.Should().NotContain(formattedErrorCode);
            cut.FindComponent<MudAlert>().Instance.Class.Should().Be(" rounded-12 my-2");
            cut.FindComponent<MudAlert>().Instance.Severity.Should().Be(Severity.Success);
            cut.FindComponent<MudIcon>().Instance.Icon.Should().Be(string.Empty);
        }
        
        [Fact]
        public void AlertMessageTest_ShowIsTrueAndErrorTypeIsError_ShouldShowMessageAndErrorCode()
        {
            string className = "class";
            string message = "message";
            string formattedErrorCode = "11_123_456";
            
            IRenderedComponent<AlertMessage> cut = RenderComponent<AlertMessage>(
                parameters => parameters
                    .Add(x => x.Class, className)
                    .Add(x => x.Message, message)
                    .Add(x => x.ErrorCode, formattedErrorCode)
                    .Add(x => x.Show, true)
                    .Add(x => x.Type, AlertType.Error)
            );

            cut.FindComponents<MudAlert>().Count.Should().Be(1);
            cut.FindComponent<MudAlert>().Markup.Should().Contain(formattedErrorCode);
            cut.FindComponent<MudAlert>().Instance.Class.Should().Be($"{className} error rounded-12 my-2");
            cut.FindComponent<MudAlert>().Instance.Severity.Should().Be(Severity.Error);
            cut.FindComponent<MudIcon>().Instance.Icon.Should().Be(Icons.Material.Filled.Error);
        }
        
        [Fact]
        public void AlertMessageTest_ShowIsTrueAndErrorTypeIsWarning_ShouldShowMessageAndErrorCode()
        {
            string className = "class";
            string message = "message";
            string formattedErrorCode = "11_123_456";
            
            IRenderedComponent<AlertMessage> cut = RenderComponent<AlertMessage>(
                parameters => parameters
                    .Add(x => x.Class, className)
                    .Add(x => x.Message, message)
                    .Add(x => x.ErrorCode, formattedErrorCode)
                    .Add(x => x.Show, true)
                    .Add(x => x.Type, AlertType.Warning)
            );

            cut.FindComponents<MudAlert>().Count.Should().Be(1);
            cut.FindComponent<MudAlert>().Markup.Should().Contain(formattedErrorCode);
            cut.FindComponent<MudAlert>().Instance.Class.Should().Be($"{className} warning rounded-12 my-2");
            cut.FindComponent<MudAlert>().Instance.Severity.Should().Be(Severity.Warning);
            cut.FindComponent<MudIcon>().Instance.Icon.Should().Be(Icons.Material.Filled.Warning);
        }
}