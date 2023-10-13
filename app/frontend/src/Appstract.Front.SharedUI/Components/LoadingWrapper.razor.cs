using Appstract.Front.Domain.Models.ApiResponseModels;
using Microsoft.AspNetCore.Components;

namespace Appstract.Front.SharedUI.Components;

public partial class LoadingWrapper
{
    [Parameter]
    public RenderFragment ContentWrapper { get; set; } = null!;

    [Parameter]
    [EditorRequired]
    public bool IsLoaded { get; set; }

    [Parameter]
    public bool HasError { get; set; }

    [Parameter]
    public Error Error { get; set; } = new ();
    
    [Parameter]
    public string Class { get; set; } = string.Empty;
}