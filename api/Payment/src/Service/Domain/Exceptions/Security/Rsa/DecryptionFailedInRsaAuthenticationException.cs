namespace Domain.Exceptions.Security.Rsa;

[Serializable]
public class DecryptionFailedInRsaAuthenticationException : SecurityException
{
    public DecryptionFailedInRsaAuthenticationException(string encBody) : base($"Decryption of body ${encBody} failed")
    {
    }
}