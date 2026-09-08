using Common.Domain.ResultMonad;

namespace Inventory.Domain.StockReservations.Errors;

public static class StockReservationErrors
{
    public static readonly Error NotReserved = new() { Key = nameof(NotReserved) };
    public static readonly Error DeadlinePassed = new() { Key = nameof(DeadlinePassed) };
    public static readonly Error CannotRelease = new() { Key = nameof(CannotRelease) };

    /// <summary>
    ///     An earlier release attempt was interrupted between calling the external warehouse system and
    ///     recording the outcome; its true state at the provider is unknown, so a new attempt is refused
    ///     rather than risking a blind double-release. Mirrors DepositPayment.AmbiguousPriorAttempt.
    /// </summary>
    public static readonly Error AmbiguousPriorAttempt = new() { Key = nameof(AmbiguousPriorAttempt) };

    public static readonly Error SeriesTooLong = new() { Key = nameof(SeriesTooLong) };

    public static readonly Error ProductNotFound = new() { Key = nameof(ProductNotFound) };

    public static readonly Error ProductStoreInactive = new() { Key = nameof(ProductStoreInactive) };
}
