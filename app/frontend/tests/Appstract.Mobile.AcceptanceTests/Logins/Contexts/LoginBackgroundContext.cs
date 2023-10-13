using Appstract.Mobile.AcceptanceTests.Common.Dto;

namespace Appstract.Mobile.AcceptanceTests.Logins.Contexts;

public class LoginBackgroundContext
{
    public List<ErrorModel> Errors { get; set; } = new();
}
