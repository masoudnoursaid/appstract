using System.ComponentModel;

namespace Appstract.Front.Domain.Enums;

public enum PaymentStatusType
{
    [Description("Failed")]
    Failed = -2,

    [Description("Successful")]
    Successful = 2,

    [Description("Cancelled")]
    Cancelled = 6,
}