namespace Domain.Exceptions.Entity;

[Serializable]
public class CurrentEntityAlreadyExistException : EntityException
{
    public CurrentEntityAlreadyExistException(string entity) : base(-1, entity, -1)
    {
    }
}