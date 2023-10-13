namespace Domain.Exceptions.Entity;

[Serializable]
public class ThereIsNoEntityWithCurrentPredicate : EntityException
{
    public ThereIsNoEntityWithCurrentPredicate(string predicate) : base(-1, predicate, -1)
    {
    }
}