using System.Runtime.Serialization;
using ErrorHandling.Abstracts;
using ErrorHandling.Enums;
using ErrorHandling.Interfaces;

namespace Infrastructure.Persistence.Sql.Exceptions;

[Serializable]
public sealed class DbUpdateConcurrencyFailedException : AppException, ICodedException
{
    public DbUpdateConcurrencyFailedException(string entityEntriesJson)
        : base("Unexpected number of affected rows when executing save changes. EntityEntries: " + entityEntriesJson)
    {
    }

    private DbUpdateConcurrencyFailedException(
        SerializationInfo info,
        StreamingContext context)
        : base(info, context)
    {
    }

    public CommonErrorCode GetCommonErrorCode()
    {
        return CommonErrorCode.DbUpdateConcurrencyFailed;
    }
}