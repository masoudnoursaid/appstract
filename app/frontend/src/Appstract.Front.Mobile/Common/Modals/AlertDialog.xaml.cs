using System.Windows.Input;
using Mopups.Interfaces;
using Mopups.Pages;
using Appstract.Mobile.Entities.Enums;

namespace Appstract.Front.Mobile.Common.Modals;

public partial class AlertDialog : PopupPage
{
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        propertyName: nameof(BtnPressedCommand),
        returnType: typeof(ICommand),
        declaringType: typeof(AlertDialog),
        defaultBindingMode: BindingMode.OneWay);

    public static readonly BindableProperty DescriptionTextProperty = BindableProperty.Create(
        propertyName: nameof(DescriptionText),
        returnType: typeof(string),
        declaringType: typeof(AlertDialog),
        defaultValue: string.Empty,
        defaultBindingMode: BindingMode.OneWay);

    public static readonly BindableProperty HeaderTextProperty = BindableProperty.Create(
        propertyName: nameof(HeaderText),
        returnType: typeof(string),
        declaringType: typeof(AlertDialog),
        defaultValue: string.Empty,
        defaultBindingMode: BindingMode.OneWay,
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            AlertDialog control = (AlertDialog)bindable;
            control.Header.Text = (string)newValue;
            control.ActionButton.Text = (string)newValue;
        });

    public static readonly BindableProperty IconImageProperty = BindableProperty.Create(
        propertyName: nameof(IconImage),
        returnType: typeof(string),
        declaringType: typeof(AlertDialog),
        defaultValue: string.Empty,
        defaultBindingMode: BindingMode.OneWay);

    public static readonly BindableProperty StateProperty = BindableProperty.Create(
        propertyName: nameof(State),
        returnType: typeof(StateDialog),
        declaringType: typeof(AlertDialog),
        defaultBindingMode: BindingMode.OneWay);

    private readonly IPopupNavigation _popupNavigation;

    public AlertDialog(IPopupNavigation popupNavigation)
    {
        InitializeComponent();
        _popupNavigation = popupNavigation;
    }

    public ICommand BtnPressedCommand
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public string DescriptionText
    {
        get => (string)GetValue(DescriptionTextProperty);
        set => SetValue(DescriptionTextProperty, value);
    }

    public string HeaderText
    {
        get => (string)GetValue(HeaderTextProperty);
        set => SetValue(HeaderTextProperty, value);
    }

    public string IconImage
    {
        get => (string)GetValue(IconImageProperty);
        set => SetValue(IconImageProperty, $"ic_{value}.svg");
    }

    public StateDialog State
    {
        get => (StateDialog)GetValue(StateProperty);
        set => SetValue(StateProperty, value);
    }

    private void Cancel_Button_Clicked(object sender, EventArgs e)
    {
        _popupNavigation.PopAsync(animate: true);
    }
}