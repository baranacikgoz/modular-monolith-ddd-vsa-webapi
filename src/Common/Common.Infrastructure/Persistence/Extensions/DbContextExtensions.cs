using System.Text.Json;
using Common.Application.AuditLog;
using Common.Application.Pagination;
using Common.Domain.Entities;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure.Persistence.Extensions;

public static class DbContextExtensions
{
    private static readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false
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
                JsonSerializer.SerializeToElement(e.Event, e.Event.GetType(), _serializerOptions),
                e.CreatedBy ?? default))
            .ToList();

        return new PaginationResponse<AuditLogDto>(items, totalCount, request.PageNumber, request.PageSize);
    }
}
