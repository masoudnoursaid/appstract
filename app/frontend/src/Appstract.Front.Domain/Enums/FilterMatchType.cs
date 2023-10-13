using System.ComponentModel;

namespace Appstract.Front.Domain.Enums;

public enum FilterMatchType
{
    [Description("Exact")]
    Exact = 1,

    [Description("Start With")]
    StartWith = 2,

    [Description("End With")]
    EndWith = 3,

    [Description("Contains")]
    Contains = 4,
}