namespace Appstract.Front.Mobile.Common.Controls;

public partial class ErrorViewControl : Frame
{

    //Error Message Text Property
    public static BindableProperty MessageProperty = BindableProperty.Create(
        propertyName: nameof(MessageProperty),
        returnType: typeof(string),
        declaringType: typeof(ErrorViewControl),
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            ErrorViewControl control = (ErrorViewControl)bindable;
            control.LblMessage.Text = (string)newValue;
        });
    public string Message
    {
        get => (string)GetValue(MessageProperty);
        set => SetValue(MessageProperty, value);
    }

    //Error Code Text Property
    public static BindableProperty CodeProperty = BindableProperty.Create(
        propertyName: nameof(CodeProperty),
        returnType: typeof(string),
        declaringType: typeof(ErrorViewControl),
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            ErrorViewControl control = (ErrorViewControl)bindable;
            control.LblCode.Text = (string)newValue;
        });
    public string Code
    {
        get => (string)GetValue(CodeProperty);
        set => SetValue(CodeProperty, value);
    }

    public ErrorViewControl()
    {
        InitializeComponent();
        LblCode.Text = Code;
        LblMessage.Text = Message;

    }
}