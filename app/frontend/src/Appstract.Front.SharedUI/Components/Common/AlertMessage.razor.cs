using Appstract.Front.Domain.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Appstract.Front.SharedUI.Components.Common;

public partial class AlertMessage
{
    [Parameter] 
    public string Class { get; set; } = string.Empty;
    [Parameter] 
    public string Message { get; set; } = string.Empty;
    [Parameter] 
    public string ErrorCode { get; set; } = string.Empty;
    [Parameter] 
    public bool Show { get; set; }

    [Parameter]
    public AlertType Type
    {
        get => _type;
        set
        {
            _type = value;
            switch (Type)
            {
                case AlertType.Error:
                    _severity = Severity.Error;
                    _icon = Icons.Material.Filled.Error;
                    _classType = $"{Class} error";
                    break;
                case AlertType.Warning:
                    _severity = Severity.Warning;
                    _icon = Icons.Material.Filled.Warning;
                    _classType = $"{Class} warning";
                    break;
                case AlertType.Info:
                    _severity = Severity.Info;
                    _icon = Icons.Material.Filled.Info;
                    _classType = $"{Class} info";
                    break;
                default:
                    _severity = Severity.Success;
                    _icon = Icons.Material.Filled.CheckCircle;
                    _classType = $"{Class} success";
                    break;
            }
        }
    }
    
    private string _icon = string.Empty;
    private string _classType = string.Empty;
    private Severity _severity = Severity.Success;
    private AlertType _type = AlertType.Success;
    
    private void CloseAlert()
    {
        Show = false;
    }
}