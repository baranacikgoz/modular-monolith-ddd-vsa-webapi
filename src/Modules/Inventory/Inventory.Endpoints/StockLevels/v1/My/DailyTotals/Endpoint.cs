using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Options;
using Common.Domain.ResultMonad;
using Inventory.Application.Persistence;
using Inventory.Application.StockLevels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using AllDailyTotals = Inventory.Endpoints.StockLevels.v1.DailyTotals;

namespace Inventory.Endpoints.StockLevels.v1.My.DailyTotals;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder stockLevelsApiGroup)
    {
        stockLevelsApiGroup
            .MapGet("my/daily-totals", GetMyDailyTotalsAsync)
            .WithDescription("The committed reservation totals per business day of one of the active stores I own, oldest first, " +
                             "with the same range rules as the administration read. A store that is not mine, or not active, has no days.")
            .RequireScope(KeycloakScopes.StockLevels.ViewOwn)
            .Produces<IReadOnlyCollection<AllDailyTotals.Response>>()
            .TransformResultTo<IReadOnlyCollection<AllDailyTotals.Response>>();
    }

    private static Task<Result<IReadOnlyCollection<AllDailyTotals.Response>>> GetMyDailyTotalsAsync(
        [AsParameters] AllDailyTotals.Request request,
        ICurrentUser currentUser,
        IInventoryDbContext dbContext,
        IOptions<InventoryOptions> options,
        TimeProvider timeProvider,
        CancellationToken cancellationToken)
    {
        return AllDailyTotals.Endpoint.ReadDaysAsync(
            dbContext.DailyTotalsOfActiveStoresOwnedBy(currentUser.Id), request, options, timeProvider, cancellationToken);
    }
}
