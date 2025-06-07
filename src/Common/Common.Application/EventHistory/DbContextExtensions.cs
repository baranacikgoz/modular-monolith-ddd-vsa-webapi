using System.Text.Json;
using Common.Application.DTOs;
using Common.Application.Pagination;
using Common.Domain.ResultMonad;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Common.Application.EventHistory;

public static class DbContextExtensions
{
    private const string ProductsModuleQuery = @"
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

    private const string IAMModuleQuery = @"
            SELECT
                ""Event"" AS ""Event"",
                ""CreatedBy"" AS ""CreatedBy"",
                COUNT(*) OVER() AS ""TotalCount""
            FROM
                ""IAM"".""EventStoreEvents""
            WHERE
                ""AggregateId"" = @id AND ""AggregateType"" = @aggregateType
            ORDER BY
                ""CreatedOn"" DESC
            OFFSET @Skip
            LIMIT @Take;
        ";

    // This is an example query for other potential modules. Reason we don't take module name as parameter, is not to create a new string each time.
    // private const string OrdersModuleQuery = @"
    //         SELECT
    //             ""Event"" AS ""Event"",
    //             ""CreatedBy"" AS ""CreatedBy"",
    //             COUNT(*) OVER() AS ""TotalCount""
    //         FROM
    //             ""Orders"".""EventStoreEvents""
    //         WHERE
    //             ""AggregateId"" = @id AND ""AggregateType"" = @aggregateType
    //         ORDER BY
    //             ""CreatedOn"" DESC
    //         OFFSET @Skip
    //         LIMIT @Take
    //     "

    public static async Task<Result<PaginationResponse<EventDto>>> GetEventHistoryAsync<TAggregate, TId>(
        this DbContext dbContext,
        string moduleName,
        TId id,
        PaginationRequest request,
        CancellationToken cancellationToken)
    {
        var query = moduleName switch
        {
            "Products" => ProductsModuleQuery,
            "IAM" => IAMModuleQuery,
            // "Orders" => OrdersModuleQuery,
            _ => throw new ArgumentOutOfRangeException(nameof(moduleName), moduleName, null)
        };

        var results = await dbContext
            .Database
            .SqlQueryRaw<PaginatedEventDto>(query,
                new NpgsqlParameter("@id", id),
                new NpgsqlParameter("@aggregateType", typeof(TAggregate).Name),
                new NpgsqlParameter("@Skip", request.Skip),
                new NpgsqlParameter("@Take", request.PageSize))
            .ToListAsync(cancellationToken);

        if (results.Count == 0)
        {
            return new PaginationResponse<EventDto>([], 0, request.PageNumber, request.PageSize);
        }
        var totalCount = results[0].TotalCount;
        var eventDtos = results.Select(x => new EventDto(x.Event, x.CreatedBy)).ToList();

        return new PaginationResponse<EventDto>(eventDtos, totalCount, request.PageNumber, request.PageSize);
    }

    private sealed record PaginatedEventDto(JsonElement Event, DefaultIdType CreatedBy, int TotalCount);
}
