using System.Runtime.Serialization;

namespace Appstract.Front.Domain.Enums;


public enum CallbackType
{
    [EnumMember(Value = "")]
    None,
    [EnumMember(Value = "PENDING")]
    Pending,
    [EnumMember(Value = "PROCESSING")]
    Processing,
    [EnumMember(Value = "PROGRESSING")]
    Progressing,
    [EnumMember(Value = "PENDING_PROCESS")]
    PendingProcess,
    [EnumMember(Value = "DUPLICATE_CANCELLED")]
    DuplicateCancelled,
    [EnumMember(Value = "INITIATED")]
    Initiated,
    [EnumMember(Value = "CHANNEL_ORIGINATED")]
    ChannelOriginated,
    [EnumMember(Value = "RINGING")]
    Ringing,
    [EnumMember(Value = "VOICEMAIL")]
    Voicemail,
    [EnumMember(Value = "USER_REJECTED")]
    UserRejected,
    [EnumMember(Value = "USER_ANSWERED")]
    UserAnswered,
    [EnumMember(Value = "SUCCESS_HANGUP")]
    SuccessHangup,
    [EnumMember(Value = "ERROR")]
    Error,
    [EnumMember(Value = "SENT")]
    Sent,
    [EnumMember(Value = "BUSY")]
    Busy,
    [EnumMember(Value = "TIME_OUT")]
    TimeOut,
    [EnumMember(Value = "CANCEL_REQUESTED")]
    CancelRequested,
    [EnumMember(Value = "CANCELLED")]
    Cancelled
}
