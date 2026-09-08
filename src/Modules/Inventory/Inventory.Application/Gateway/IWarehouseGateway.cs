using Common.Domain.ResultMonad;

namespace Inventory.Application.Gateway;

/// <summary>
///     3rd-party warehouse system integration, paired with <c>StockReservation.BeginReleaseAttempt</c> /
///     <c>AbandonReleaseAttempt</c>: the aggregate's attempt marker guards against a blind double-release,
///     this gateway is the actual outbound call made while that marker is held.
/// </summary>
public interface IWarehouseGateway
{
    /// <summary>Requests the warehouse release the hold on <paramref name="quantity"/> units. Returns the
    /// provider's confirmation reference on success.</summary>
    Task<Result<string>> RequestReleaseAsync(string productReference, int quantity, CancellationToken cancellationToken);

    /// <summary>
    ///     Idempotent read-only call to resolve the true outcome of a release request after an AMBIGUOUS
    ///     failure (timeout, connection reset) from <see cref="RequestReleaseAsync"/> - never call this to
    ///     retry a call that got a definite rejection, and never blindly retry <see cref="RequestReleaseAsync"/>
    ///     itself after an ambiguous failure without reconciling first.
    /// </summary>
    Task<Result<string>> ReconcileReleaseAsync(string productReference, CancellationToken cancellationToken);
}
