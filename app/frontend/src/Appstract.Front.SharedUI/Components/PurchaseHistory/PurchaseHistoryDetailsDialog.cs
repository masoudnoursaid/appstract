using Appstract.Front.Application.Common.Constants;
using Appstract.Front.Domain.Enums;
using Appstract.Front.Domain.Models.PurchaseHistory;
using Appstract.Front.Infrastructure.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Appstract.Front.SharedUI.Components.PurchaseHistory;

public partial class PurchaseHistoryDetailsDialog
{
    [CascadingParameter]
    private MudDialogInstance MudDialog { get; set; } = null!;

    [Inject]
    private BaseInfoServices BaseInfo { get; set; } = null!;

    [Parameter]
    public PurchaseHistoryDetailsModel Content { get; set; } = null!;

    private string SvgImage => Content.PaymentStatusName switch
    {
        PaymentStatusType.Successful => UltraToneIcons.PURCHASE_HISTORY_SUCCESS,
        PaymentStatusType.Failed =>  UltraToneIcons.PURCHASE_HISTORY_FAILED,
        PaymentStatusType.Cancelled =>  UltraToneIcons.PURCHASE_HISTORY_CANCELED,
        _ => string.Empty
    };

    private Color StatusColor => Content.PaymentStatusName switch
    {
        PaymentStatusType.Successful => Color.Success,
        PaymentStatusType.Failed => Color.Error,
        PaymentStatusType.Cancelled => Color.Dark,
        _ => Color.Dark
    };
    
    private void OnCloseClick() => MudDialog.Cancel();
}