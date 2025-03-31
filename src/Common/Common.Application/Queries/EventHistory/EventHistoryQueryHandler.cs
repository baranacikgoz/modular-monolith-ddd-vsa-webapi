using System.Text.Json;
using Common.Application.CQS;
using Common.Application.DTOs;
using Common.Application.Persistence;
using Common.Application.Queries.Pagination;
using Common.Domain.Aggregates;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Common.Application.Queries.EventHistory;

public abstract class EventHistoryQueryHandler<TAggregate, TDbContext>(TDbContext dbContext) : IQueryHandler<EventHistoryQuery<TAggregate, TDbContext>, PaginationResult<EventDto>>
    where TDbContext : IDbContext
    where TAggregate : class, IAggregateRoot
{
    public required IStronglyTypedId AggregateId { get; init; }

    public async Task<Result<PaginationResult<EventDto>>> Handle(EventHistoryQuery<TAggregate, TDbContext> request, CancellationToken cancellationToken)
    {
        const string Query = @"
            SELECT
                ""Event"" AS ""Event"",
                ""CreatedBy"" AS ""CreatedBy"",
                COUNT(*) OVER() AS ""TotalCount""
            FROM
                ""Products"".""EventStoreEvents""
            WHERE
                ""AggregateId"" = @id AND ""AggregateType"" = @aggregateType
            ORDER BY
                ""CreatedOn"" DESC
            OFFSET @Skip
            LIMIT @Take;
        ";

        var results = await dbContext
        .Database
        .SqlQueryRaw<PaginatedEventDto>(Query,
            new NpgsqlParameter("@id", request.AggregateId.Value),
            new NpgsqlParameter("@aggregateType", typeof(TAggregate).Name),
            new NpgsqlParameter("@Skip", request.Skip),
            new NpgsqlParameter("@Take", request.PageSize))
        .ToListAsync(cancellationToken);

        if (results.Count == 0)
        {
            return new PaginationResult<EventDto>([], 0, request.PageNumber, request.PageSize);
        }
        var totalCount = results[0].TotalCount;
        var eventDtos = results.Select(x => new EventDto(x.Event, x.CreatedBy)).ToList();

        return new PaginationResult<EventDto>(eventDtos, totalCount, request.PageNumber, request.PageSize);
    }

    private sealed record PaginatedEventDto(JsonElement Event, DefaultIdType CreatedBy, int TotalCount);
}
