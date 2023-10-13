namespace Appstract.Mobile.Application.Dto.Authentication;

public class VerifyEmailOtpDto
{
    public bool? NewUser { get; set; }
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}
