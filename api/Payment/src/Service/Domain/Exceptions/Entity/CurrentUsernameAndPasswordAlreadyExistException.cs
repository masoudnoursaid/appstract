namespace Domain.Exceptions.Entity;

[Serializable]
public class CurrentUsernameAndPasswordAlreadyExistException : EntityException
{
    public CurrentUsernameAndPasswordAlreadyExistException(string username, string password) : base(-1,
        $"{username}:{password}", -1)
    {
    }
}