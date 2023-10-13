using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common.BaseTypes;

public interface IBaseEntity<TId> where TId : class
{
    TId Id { get; set; }
    DateTime? ModifiedDateTime { get; set; }
    DateTime CreatedDateTime { get; }
    DateTime? DeletedDateTime { get; }
}

public class BaseEntity : BaseEntity<string>
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public virtual int Counter { get; set; }
}

public abstract class BaseEntity<T> : IBaseEntity<T>
    where T : class
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual T Id { get; set; } = null!;

    public virtual DateTime? ModifiedDateTime { get; set; }
    public virtual DateTime CreatedDateTime { get; } = DateTime.UtcNow;
    public virtual DateTime? DeletedDateTime { get; private set; }

    public virtual void SoftDelete()
    {
        DeletedDateTime = DateTime.UtcNow;
    }
}