namespace Application.Common.Uri;

public interface IMollieUri : IGlobalPayUri
{
    System.Uri RedirectVerify { get; }
    System.Uri CallBackVerify { get; }
}