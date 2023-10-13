using Appstract.Front.Application.Common.Constants;
using MudBlazor;

namespace Appstract.Front.SharedUI.Shared;

public partial class MainLayout
{
    private readonly MudTheme _ultraToneTheme = new()
    {
        Palette = new PaletteLight
        {
            Primary = UltraToneColors.Purple.Default,
            Success = UltraToneColors.Green.Default,
            Warning = UltraToneColors.Yellow.Default,
            Error = UltraToneColors.Red.Default,
            Secondary = UltraToneColors.Grey.Lighten6,
            Tertiary = UltraToneColors.Grey.Lighten1,
            AppbarBackground = UltraToneColors.Grey.Lighten0,
        },
        Typography = new Typography { Button = new Button { TextTransform = "none" } }
    };
}