using System.Net;
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
    ///     rather than risking a blind double-release.
    /// </summary>
    public static readonly Error AmbiguousPriorAttempt = new() { Key = nameof(AmbiguousPriorAttempt) };

    /// <summary>
    ///     The provider reported a status this system has no mapping for, so the reservation no longer follows it and takes no
    ///     local command until someone resolves it by hand.
    /// </summary>
    public static readonly Error ReservationReadOnly = new()
    {
        Key = nameof(ReservationReadOnly),
        StatusCode = HttpStatusCode.Conflict
    };

    public static readonly Error ProviderReferenceRequired = new() { Key = nameof(ProviderReferenceRequired) };

    public static readonly Error SeriesTooLong = new() { Key = nameof(SeriesTooLong) };

    public static readonly Error ProductNotFound = new() { Key = nameof(ProductNotFound) };

    public static readonly Error ProductStoreInactive = new() { Key = nameof(ProductStoreInactive) };
}
