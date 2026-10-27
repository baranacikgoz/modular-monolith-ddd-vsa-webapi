using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Options;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Inventory.Application.Persistence;
using Inventory.Domain.StockCounters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Inventory.Endpoints.StockLevels.v1.DailyTotals;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder stockLevelsApiGroup)
    {
        stockLevelsApiGroup
            .MapGet("daily-totals", GetDailyTotalsAsync)
            .WithDescription("A store's committed reservation totals per business day, oldest first. From and To are calendar days in the " +
                             "business time zone, both included; To defaults to today there. The range is capped by " +
                             "InventoryOptions.DailyTotalsMaxRangeDays. A day with no committed reservation has no row. " +
                             "Administration only; a store owner reads their own store's totals through /my/daily-totals.")
            .RequireScope(KeycloakScopes.StockLevels.ViewAll)
            .Produces<IReadOnlyCollection<Response>>()
            .TransformResultTo<IReadOnlyCollection<Response>>();
    }

    private static Task<Result<IReadOnlyCollection<Response>>> GetDailyTotalsAsync(
        [AsParameters] Request request,
        IInventoryDbContext dbContext,
        IOptions<InventoryOptions> options,
        TimeProvider timeProvider,
        CancellationToken cancellationToken)
    {
        return ReadDaysAsync(dbContext.StoreDailyStockTotals, request, options, timeProvider, cancellationToken);
    }

    /// <summary>
    ///     The days of the store <paramref name="request" /> names, read inside <paramref name="totals" />: every store's totals
    ///     for the administration route, the caller's own stores' for the store owner's route, where a store outside the set
    ///     has no days.
    /// </summary>
    internal static async Task<Result<IReadOnlyCollection<Response>>> ReadDaysAsync(
        IQueryable<StoreDailyStockTotal> totals,
        Request request,
        IOptions<InventoryOptions> options,
        TimeProvider timeProvider,
        CancellationToken cancellationToken)
    {
        var lastDay = request.LastDay(timeProvider, TimeZoneInfo.FindSystemTimeZoneById(options.Value.BusinessTimeZoneId));

        IReadOnlyCollection<Response> days = await totals
            .AsNoTracking()
            .TagWith(nameof(ReadDaysAsync), request.StoreId)
            .Where(t => t.StoreId == request.StoreId && t.Day >= request.From && t.Day <= lastDay)
            .OrderBy(t => t.Day)
            .Select(t => new Response
            {
                Day = t.Day,
                CommittedReservations = t.CommittedReservations,
                CommittedUnits = t.CommittedUnits
            })
            .ToListAsync(cancellationToken);

        return Result<IReadOnlyCollection<Response>>.Success(days);
    }
}
