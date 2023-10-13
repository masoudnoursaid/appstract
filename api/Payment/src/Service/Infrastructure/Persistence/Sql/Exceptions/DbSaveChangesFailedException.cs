using System.Runtime.Serialization;
using ErrorHandling.Abstracts;
using ErrorHandling.Enums;
using ErrorHandling.Interfaces;

namespace Infrastructure.Persistence.Sql.Exceptions;

[Serializable]
public class DbSaveChangesFailedException : AppException, ICodedException
{
    public DbSaveChangesFailedException(string message, Exception inner)
        : base(message, inner)
    {
    }

    protected DbSaveChangesFailedException(
        SerializationInfo info,
        StreamingContext context)
        : base(info, context)
    {
    }

    public CommonErrorCode GetCommonErrorCode()
    {
        return CommonErrorCode.SaveChangesFailed;
    }
}