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
    ///     reservation), never auto-resolved, needs manual reconciliation. Mirrors DepositPayment's
    ///     RequiresRefund branch for a detected double-charge.
    /// </summary>
    RequiresReconciliation
}
