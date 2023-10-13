using System.Runtime.Serialization;

namespace Application.Common.Uri;

public interface IGlobalPayUri : ISerializable
{
    System.Uri Cancel { get; }
}