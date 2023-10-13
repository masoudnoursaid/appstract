namespace Application.Common.Uri;

public interface IPaypalUri : IGlobalPayUri
{
    System.Uri Verify { get; }
}