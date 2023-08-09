using System.Data.Common;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore.Diagnostics;
using DbConnectionInterceptorBase = Microsoft.EntityFrameworkCore.Diagnostics.DbConnectionInterceptor;

namespace Infrastructure.Interceptors;

public class DbConnectionInterceptor : DbConnectionInterceptorBase
{
    /// <inheritdoc />
    public override void ConnectionFailed(DbConnection connection, ConnectionErrorEventData eventData)
    {
        throw new DbConnectionFailedException(eventData.Exception.Message, eventData.Exception);
    }

    /// <inheritdoc />
    public override Task ConnectionFailedAsync(DbConnection connection, ConnectionErrorEventData eventData, CancellationToken cancellationToken = default)
    {
        throw new DbConnectionFailedException(eventData.Exception.Message, eventData.Exception);
    }
}