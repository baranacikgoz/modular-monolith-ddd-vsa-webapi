namespace Inventory.Domain.StockReservations;

public enum ReservationStatus
{
    Active,
    Committed,
    Released,
    Expired,

    /// <summary>
    ///     A Commit arrived with a different provider reference than the one already recorded on this
    ///     Committed reservation - a genuine conflict (two different external confirmations for the same
    ///     reservation), never auto-resolved, needs manual reconciliation.
    /// </summary>
    RequiresReconciliation,

    /// <summary>
    ///     What the provider's status is recorded as when it reported a word this system has no mapping for. Only ever the
    ///     value of <see cref="StockReservation.ProviderStatus" />: a reservation's own <see cref="StockReservation.Status" />
    ///     never takes it, and no transition leads to or from it.
    /// </summary>
    Unknown
}
