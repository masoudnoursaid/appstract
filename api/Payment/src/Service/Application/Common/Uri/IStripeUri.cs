namespace Application.Common.Uri;

public interface IStripeUri : IGlobalPayUri
{
    System.Uri RedirectVerify { get; }
    System.Uri CallBackVerify { get; }
}
