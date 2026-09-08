using System.Text.Json;
using System.Text.Json.Serialization;
using Common.Application.AuditLog;
using Common.Application.JsonConverters;
using Common.Application.Pagination;
using Common.Application.Persistence;
using Common.Domain.Entities;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Common.Infrastructure.Persistence.Extensions;

public static class DbContextExtensions
{
    private const string PostgresUniqueViolationSqlState = "23505";

    /// <summary>
    ///     Saves changes, translating a Postgres unique-constraint violation into <paramref name="conflictError"/>
    ///     instead of letting <see cref="DbUpdateException"/> bubble up as an unhandled 500. An app-level
    ///     pre-check (e.g. <c>AnyAsResultAsync</c> before insert) narrows the common case to a clean early
    ///     failure, but only a DB constraint is race-safe under concurrent requests: two requests can both pass
    ///     the pre-check before either commits. This is the backstop for that race, not a replacement for the
    ///     pre-check - callers keep both, mirroring an app-level guard paired with a DB exclusion constraint.
    /// </summary>
    public static async Task<Result> SaveChangesDetectingUniqueViolationAsync(
        this IDbContext dbContext,
        Error conflictError,
        CancellationToken cancellationToken)
    {
        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success;
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException { SqlState: PostgresUniqueViolationSqlState })
        {
            return conflictError;
        }
    }

    private static readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false,
        Converters =
        {
            new StronglyTypedIdWriteOnlyJsonConverter(),
            new JsonStringEnumConverter()
        }
    };

    public static async Task<Result<PaginationResponse<AuditLogDto>>> GetAuditLogAsync<TAggregate, TId>(
        this DbSet<AuditLogEntry> auditLog,
        TId id,
        PaginationRequest request,
        CancellationToken cancellationToken) where TId : IStronglyTypedId
    {
        var aggregateType = typeof(TAggregate).Name;

        var query = auditLog
            .AsNoTracking()
            .Where(e => e.AggregateId == id.Value && e.AggregateType == aggregateType);

        var totalCount = await query.CountAsync(cancellationToken);

        var entries = await query
            .OrderByDescending(e => e.CreatedOn)
            .Skip(request.Skip)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var items = entries
            .Select(e => new AuditLogDto(
                e.CreatedOn,
                e.EventType,
                e.Version,
                JsonSerializer.SerializeToElement(e.Event, e.Event.GetType(), _serializerOptions),
                e.CreatedBy ?? default))
            .ToList();

        return new PaginationResponse<AuditLogDto>(items, totalCount, request.PageNumber, request.PageSize);
    }
}
