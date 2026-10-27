using Common.Domain.ResultMonad;
using Inventory.Domain.StockReservations;

namespace Inventory.Application.Gateway;

/// <summary>
///     3rd-party warehouse system integration for ONE connection, paired with <c>StockReservation.BeginReleaseAttempt</c> /
///     <c>AbandonReleaseAttempt</c>: the aggregate's attempt marker guards against a blind double-release,
///     this gateway is the actual outbound call made while that marker is held. Obtained from
///     <see cref="IWarehouseGatewayFactory" />, which binds it to the connection's provider, address and credential.
///     <para>
///         Nothing here is retried inside a request. A failed first attempt is never sent a second time, because the
///         release changes state at the warehouse and a timeout does not say whether it happened. A <see cref="Result" />
///         failure is a definite outcome (the warehouse answered, or our own quota or open circuit stopped the call before
///         anything was sent); an exception (timeout, connection reset) is ambiguous and must be resolved with
///         <see cref="ReconcileReleaseAsync" />.
///     </para>
///     <para>
///         <see cref="PushStockAsync" /> follows the same rules but needs no reconcile call: it sets absolute quantities, so
///         after an unknown outcome the caller simply sends the then current quantities again.
///     </para>
/// </summary>
public interface IWarehouseGateway
{
    /// <summary>
    ///     Requests the warehouse release the hold on <paramref name="quantity" /> units of <paramref name="productReference" />
    ///     taken by the reservation <paramref name="reservationId" />. The reservation id travels as the provider-facing
    ///     reference and idempotency key of the release, so the warehouse can tell two reservations of the same product apart
    ///     and <see cref="ReconcileReleaseAsync" /> asks about exactly this one. Returns the provider's confirmation reference
    ///     on success.
    /// </summary>
    Task<Result<string>> RequestReleaseAsync(
        StockReservationId reservationId, string productReference, int quantity, CancellationToken cancellationToken);

    /// <summary>
    ///     Idempotent read-only call to resolve the true outcome of the release of <paramref name="reservationId" /> after an
    ///     AMBIGUOUS failure (timeout, connection reset) from <see cref="RequestReleaseAsync"/>. Never call this to
    ///     retry a call that got a definite rejection, and never blindly retry <see cref="RequestReleaseAsync"/>
    ///     itself after an ambiguous failure without reconciling first. A failed result with
    ///     <see cref="WarehouseGatewayErrors.ReferenceNotFound" /> is the warehouse saying, explicitly, that it knows no release
    ///     for this reservation; a plain 404 is not that, it is <see cref="WarehouseGatewayErrors.EndpointNotFound" />.
    /// </summary>
    Task<Result<string>> ReconcileReleaseAsync(StockReservationId reservationId, CancellationToken cancellationToken);

    /// <summary>
    ///     Sets the quantity the warehouse shows for each product in <paramref name="quantities" /> in one call. Returns the
    ///     provider's reference of the batch on success. Safe to send again with fresh quantities, never retried by the pipeline.
    /// </summary>
    Task<Result<string>> PushStockAsync(IReadOnlyCollection<WarehouseStockQuantity> quantities, CancellationToken cancellationToken);
}
