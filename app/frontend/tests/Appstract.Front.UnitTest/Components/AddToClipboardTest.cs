using System;
using System.Linq;
using Appstract.TestCommon.Base;
using Appstract.TestCommon.Viewers;
using FluentAssertions;
using MudBlazor.Components.Snackbar.InternalComponents;

namespace Appstract.UnitTest.Components
{
    public class AddToClipboardTest : ComponentTestContext
    {
        [Fact]
        public void AddToClipboard_WhenClickOnCopyButton_ShouldShowMessageAndCopyText()
        {
            IRenderedComponent<CopyToClipboardViewer> cut = RenderComponent<CopyToClipboardViewer>(
                parameters => parameters
                    .Add(x => x.Text, "Sample Text")
            );

            cut.Find(".text-wrapper__copy-btn").Click();

            cut.FindComponents<SnackbarMessageText>().Any().Should().BeTrue();
            cut.WaitForAssertion(() =>
            {
                cut.FindComponents<SnackbarMessageText>().Any().Should().BeFalse();
            }, TimeSpan.FromMilliseconds(5000));
        }
    }
}