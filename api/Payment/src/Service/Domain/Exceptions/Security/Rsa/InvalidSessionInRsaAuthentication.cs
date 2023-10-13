namespace Domain.Exceptions.Security.Rsa;

[Serializable]
public class InvalidSessionInRsaAuthentication : SecurityException
{
    public InvalidSessionInRsaAuthentication(string token) : base(
        $"Session with token {token} not found. PV key can not retrieve")
    {
    }
}