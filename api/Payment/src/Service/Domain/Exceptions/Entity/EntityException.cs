using ErrorHandling.Abstracts;
using ErrorHandling.Enums;
using ErrorHandling.Interfaces;

namespace Domain.Exceptions.Entity;

[Serializable]
public class EntityException : AppException, ICodedException
{
    public EntityException(int entityId, string predicate, int? code = null)
    {
        EntityId = entityId;
        Predicate = predicate;
    }

    public EntityException(object? key, int? code = null)
    {
        Key = key;
    }

    public object? Key { get; set; }
    public int? EntityId { get; set; }
    public string? Predicate { get; set; }

    public CommonErrorCode GetCommonErrorCode()
    {
        return CommonErrorCode.EntityError;
    }
}