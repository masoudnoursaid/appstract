using Appstract.Front.SharedUI.Components;
using Appstract.TestCommon.Base;
using FluentAssertions;

namespace Appstract.UnitTest.Components
{
    public class ContentsWrapperTest : ComponentTestContext
    {
        [Fact]
        public void ContentWrapper_PassParameters_ShouldRenderParameters()
        {
            string title = "Test Title";
            string childContent = "<p>Test Child</p>";

            IRenderedComponent<ContentsWrapper> cut = RenderComponent<ContentsWrapper>(
                parameters => parameters
                .Add(x => x.Title, title)
                .AddChildContent(childContent)
            );

            cut.Find("h1").TextContent.MarkupMatches(title);
            cut.Find("p").MarkupMatches(childContent);
        }
        
        [Fact]
        public void ContentWrapper_PassClassParameter_ShouldRenderClass()
        {
            string className = "sample-class";

            IRenderedComponent<ContentsWrapper> cut = RenderComponent<ContentsWrapper>(
                parameters => parameters
                .Add(x => x.Class, className)
            );

            cut.Find("div").ClassList.Should().Contain(className);
        }
    }
}