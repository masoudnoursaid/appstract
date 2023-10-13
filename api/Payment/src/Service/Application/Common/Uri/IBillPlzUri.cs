namespace Application.Common.Uri;

public interface IBillPlzUri : IGlobalPayUri
{
    System.Uri RedirectVerify { get; }
    System.Uri CallBackVerify { get; }
}