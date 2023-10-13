using System.Data.Common;
using Infrastructure.Persistence.Sql.Exceptions;
using Microsoft.EntityFrameworkCore.Diagnostics;
using DbConnectionInterceptorBase = Microsoft.EntityFrameworkCore.Diagnostics.DbConnectionInterceptor;

namespace Infrastructure.Persistence.Sql.Interceptors;

public class DbConnectionInterceptor : DbConnectionInterceptorBase
{
    public override void ConnectionFailed(DbConnection connection, ConnectionErrorEventData eventData)
    {
        throw new DbConnectionFailedException(eventData.Exception.Message, eventData.Exception);
    }

    public override Task ConnectionFailedAsync(DbConnection connection, ConnectionErrorEventData eventData,
        CancellationToken cancellationToken = default)
    {
        throw new DbConnectionFailedException(eventData.Exception.Message, eventData.Exception);
    }
}