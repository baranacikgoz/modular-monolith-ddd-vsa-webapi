using Inventory.Infrastructure.StockReservations;

namespace Inventory.Endpoints.StockReservations.v1.ReconcileRelease;

public sealed record Response
{
    /// <summary>
    ///     What the warehouse system said became of the release: Released (completed locally), NotReleased (the attempt marker
    ///     was abandoned, so a release can be tried again) or AlreadyReleased (nothing was asked).
    /// </summary>
    public required ReleaseReconcileOutcome Outcome { get; init; }
}
