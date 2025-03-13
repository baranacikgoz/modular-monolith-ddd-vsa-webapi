using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Common.Application.DTOs;
using Common.Application.Queries.Pagination;
using Common.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Common.Application.Queries.EventHistory;

public static class DbContextExtensions
{
    public static async Task<PaginationResult<EventDto>> GetEventHistoryAsync<TAggregate>(this DbContext context, EventHistoryQuery<TAggregate> query, CancellationToken cancellationToken)
        where TAggregate : class, IAggregateRoot
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

        var results = await context
        .Database
        .SqlQueryRaw<PaginatedEventDto>(Query,
            new NpgsqlParameter("@id", query.AggregateId.Value),
            new NpgsqlParameter("@aggregateType", typeof(TAggregate).Name),
            new NpgsqlParameter("@Skip", query.Skip),
            new NpgsqlParameter("@Take", query.PageSize))
        .ToListAsync(cancellationToken);

        if (results.Count == 0)
        {
            return new PaginationResult<EventDto>([], 0, query.PageNumber, query.PageSize);
        }
        var totalCount = results[0].TotalCount;
        var eventDtos = results.Select(x => new EventDto(x.Event, x.CreatedBy)).ToList();

        return new PaginationResult<EventDto>(eventDtos, totalCount, query.PageNumber, query.PageSize);
    }

    private sealed record PaginatedEventDto(JsonElement Event, DefaultIdType CreatedBy, int TotalCount);
}
