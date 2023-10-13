using System.Text.Json;
using Infrastructure.Persistence.Sql.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SaveChangesInterceptorBase = Microsoft.EntityFrameworkCore.Diagnostics.SaveChangesInterceptor;

namespace Infrastructure.Persistence.Sql.Interceptors;

public class SaveChangesInterceptor : SaveChangesInterceptorBase
{
    public override void SaveChangesFailed(DbContextErrorEventData eventData)
    {
        ThrowIfIsDbUpdateConcurrencyException(eventData);

        throw new DbSaveChangesFailedException(eventData.Exception.Message, eventData.Exception);
    }

    public override Task SaveChangesFailedAsync(DbContextErrorEventData eventData,
        CancellationToken cancellationToken = default)
    {
        ThrowIfIsDbUpdateConcurrencyException(eventData);

        throw new DbSaveChangesFailedException(eventData.Exception.Message, eventData.Exception);
    }

    private static void ThrowIfIsDbUpdateConcurrencyException(DbContextErrorEventData eventData)
    {
        if (eventData.Exception is DbUpdateConcurrencyException ex)
            throw new DbUpdateConcurrencyFailedException(
                JsonSerializer.Serialize(ex.Entries.Select(x => x.ToString())));
    }
}