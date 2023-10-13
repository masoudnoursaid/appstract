namespace Domain.Exceptions.Entity;

[Serializable]
public class CanNotFoundEntityException : EntityException
{
    public CanNotFoundEntityException(int entityId, string predicate) : base(entityId, predicate, -1)
    {
    }

    public CanNotFoundEntityException(object? key) : base(key, -1)
    {
    }

    public CanNotFoundEntityException(string predicate) : base(predicate, -1)
    {
    }
}