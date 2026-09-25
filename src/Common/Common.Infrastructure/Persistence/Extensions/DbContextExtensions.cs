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

    /// <summary>
    ///     One aggregate's history, newest first: <c>CreatedOn</c> descending, then <c>Version</c> descending. The order is
    ///     total (<c>Version</c> is unique per aggregate) and is a published contract: rows one command wrote at the same
    ///     instant come back in a fixed order across pages.
    ///     <para>
    ///         Offset paging (<see cref="PaginationRequest.PageNumber" />) stays the default. A full page also carries
    ///         <see cref="PaginationResponse{T}.NextCursor" />; passing it back as <see cref="PaginationRequest.After" />
    ///         continues right after that row with <c>LIMIT</c> only, so a deep page costs the same as the first one
    ///         (a history that keeps growing, like a wallet's, otherwise pays <c>OFFSET</c> and a <c>COUNT</c> per page).
    ///         <see cref="PaginationRequest.IncludeTotal" /> false skips the count. A malformed cursor is user input and
    ///         fails as a <c>Validation</c> error. <see cref="PaginateAsync" /> cannot serve this order: it sorts on one
    ///         key plus an ascending tiebreaker.
    ///     </para>
    /// </summary>
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

        var totalCount = request.ShouldIncludeTotal ? await query.CountAsync(cancellationToken) : -1;

        var useCursor = request.After is not null;
        if (useCursor)
        {
            var bound = DecodeAuditLogCursor(request.After!);
            if (bound.IsFailure)
            {
                return bound.Error!;
            }

            var (createdOn, version) = bound.Value;
            query = query.Where(e => e.CreatedOn < createdOn || (e.CreatedOn == createdOn && e.Version < version));
        }

        var ordered = query
            .OrderByDescending(e => e.CreatedOn)
            .ThenByDescending(e => e.Version); // one command can raise several events at the same instant: keep page boundaries stable

        var entries = await (useCursor ? ordered.Take(request.Take) : ordered.Skip(request.Skip).Take(request.Take))
            .ToListAsync(cancellationToken);

        var items = entries
            .Select(e => new AuditLogDto(
                e.CreatedOn,
                e.EventType,
                e.Version,
                JsonSerializer.SerializeToElement(e.Event, e.Event.GetType(), _serializerOptions),
                e.CreatedBy))
            .ToList();

        var nextCursor = entries.Count == request.PageSize
            ? PaginationCursor.Encode(entries[^1].CreatedOn, entries[^1].Version)
            : null;

        return new PaginationResponse<AuditLogDto>(items, totalCount, useCursor ? 1 : request.PageNumber, request.PageSize, nextCursor);
    }

    /// <summary>The (CreatedOn, Version) of the last row of the previous page; a cursor minted for another sort is invalid.</summary>
    private static Result<(DateTimeOffset CreatedOn, long Version)> DecodeAuditLogCursor(string cursor)
    {
        var decoded = PaginationCursor.Decode(cursor);
        if (decoded.IsFailure)
        {
            return decoded.Error!;
        }

        return decoded.Value is { SortValue: DateTimeOffset createdOn, Tiebreaker: long version }
            ? (createdOn, version)
            : Error.Validation([PaginationCursor.ParameterName]);
    }
}
