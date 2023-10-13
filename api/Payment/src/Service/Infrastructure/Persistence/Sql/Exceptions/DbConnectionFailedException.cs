using System.Runtime.Serialization;
using ErrorHandling.Abstracts;
using ErrorHandling.Enums;
using ErrorHandling.Interfaces;

namespace Infrastructure.Persistence.Sql.Exceptions;

[Serializable]
public class DbConnectionFailedException : AppException, ICodedException
{
    public DbConnectionFailedException(string message, Exception inner)
        : base(message, inner)
    {
    }

    protected DbConnectionFailedException(
        SerializationInfo info,
        StreamingContext context)
        : base(info, context)
    {
    }

    public CommonErrorCode GetCommonErrorCode()
    {
        return CommonErrorCode.DatabaseConnectionFailed;
    }
}