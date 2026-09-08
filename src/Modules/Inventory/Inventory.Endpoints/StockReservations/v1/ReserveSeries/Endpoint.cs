using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Options;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Extensions;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Products;
using Inventory.Application.Persistence;
using Inventory.Domain.StockReservations;
using Inventory.Domain.StockReservations.Errors;
using Inventory.Infrastructure.Telemetry;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;

namespace Inventory.Endpoints.StockReservations.v1.ReserveSeries;

/// <summary>
///     Flagship orchestration endpoint: policy guard, a cross-module sync call to Products (Inventory
///     calling out, mirroring Products' own inbound call to Inventory in the Get endpoint), Options-driven
///     tunables, a recurrence-style generation algorithm, and a sequence-dependent field on the first
///     generated item - the full multi-step shape, ported generically (not copied from any real booking
///     domain).
/// </summary>
internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder stockReservationsApiGroup)
    {
        stockReservationsApiGroup
            .MapPost("series", ReserveSeriesAsync)
            .WithDescription("Reserve stock for a recurring series of pending orders against the same product.")
            .RequireScope(KeycloakScopes.StockReservations.Create)
            .Produces<Response>()
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> ReserveSeriesAsync(
        Request request,
        IInventoryDbContext dbContext,
        IInterModuleRequestClient<GetProductRequest, GetProductResponse> productClient,
        IOptions<InventoryOptions> inventoryOptions,
        TimeProvider timeProvider,
        CancellationToken cancellationToken)
    {
        using var activity = InventoryTelemetry.ActivitySource.StartActivityForCaller();

        var options = inventoryOptions.Value;
        var body = request.Body;

        // Policy guards, folded into the pipeline's starting value rather than an early imperative return -
        // both checked before anything external is touched.
        Result<Request.RequestBody> initial = body;
        if (body.OccurrenceCount > options.MaxOccurrencesPerSeries)
        {
            initial = StockReservationErrors.SeriesTooLong;
        }
        else if (body.FirstDeadline < timeProvider.GetUtcNow())
        {
            initial = StockReservationErrors.DeadlinePassed;
        }

        return await initial
            // Cross-module sync call to Products - the missing direction, Inventory calling out this time.
            .BindAsync(async b => Result<GetProductResponse>.Success(
                await productClient.SendAsync(new GetProductRequest(b.ProductId), cancellationToken)))
            .TapAsync(product => string.IsNullOrEmpty(product.Name)
                ? StockReservationErrors.ProductNotFound
                : Result.Success)
            .TapAsync(product => !product.StoreIsActive
                ? StockReservationErrors.ProductStoreInactive
                : Result.Success)
            .MapAsync(_ => BuildReservations(body, options))
            .TapAsync(reservations => dbContext.StockReservations.AddRange(reservations))
            .TapAsync(async _ => await dbContext.SaveChangesAsync(cancellationToken))
            .TapAsync(_ => InventoryTelemetry.ReservationSeriesCreated.Add(1))
            .MapAsync(reservations => new Response { Ids = reservations.Select(r => r.Id).ToList() })
            .TapActivityAsync(activity);
    }

    /// <summary>
    ///     Recurrence-style generation: builds one <see cref="StockReservation"/> per occurrence, spaced
    ///     <see cref="Request.RequestBody.IntervalDays"/> apart from <see cref="Request.RequestBody.FirstDeadline"/>.
    ///     Only the first occurrence carries the safety-buffer quantity - a sequence-dependent field assignment,
    ///     mirroring CreateManualBooking's "only the first item in a batch gets a special value" shape. The
    ///     per-warehouse <see cref="Request.RequestBody.Warehouses"/> allocation is request-shape validation only
    ///     in this phase (see RequestValidator's cross-collection sum check) - it doesn't yet have its own
    ///     domain representation; each generated reservation covers the whole product/quantity.
    /// </summary>
    private static List<StockReservation> BuildReservations(Request.RequestBody body, InventoryOptions options)
    {
        var reservations = new List<StockReservation>(body.OccurrenceCount);

        for (var index = 0; index < body.OccurrenceCount; index++)
        {
            var deadline = body.FirstDeadline.AddDays(body.IntervalDays * index);
            var quantity = index == 0
                ? body.QuantityPerOccurrence + options.SafetyBufferQuantity
                : body.QuantityPerOccurrence;

            reservations.Add(StockReservation.Reserve(body.ProductId, quantity, deadline));
        }

        return reservations;
    }
}
