using Appstract.Front.SharedUI.Components.Common;
using Appstract.TestCommon.Base;
using FluentAssertions;

namespace Appstract.UnitTest.Components;

public class MainTitleTest : ComponentTestContext
{
    [Fact]
    public void MainTitle_PassTitle_ShouldRenderTitle()
    {
        string title = "Test Title";

        IRenderedComponent<MainTitle> cut = RenderComponent<MainTitle>(parameters => parameters
            .Add(x => x.Title, title)
        );
        
        cut.Find("h1").TextContent.MarkupMatches(title);
    }
    
    [Fact]
    public void MainTitle_PassClass_ShouldRenderClass()
    {
        string className = "sample-class";

        IRenderedComponent<MainTitle> cut = RenderComponent<MainTitle>(parameters => parameters
            .Add(x => x.Class, className)
        );

        cut.Find("h1").ClassList.Should().Contain(className);
    }
}