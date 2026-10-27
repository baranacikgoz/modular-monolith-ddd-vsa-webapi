using System.Net;
using Common.Domain.ResultMonad;

namespace Inventory.Domain.WarehouseConnections.Errors;

public static class WarehouseConnectionErrors
{
    public static readonly Error CannotChangeWhileDeactivated = new() { Key = nameof(CannotChangeWhileDeactivated) };

    /// <summary>
    ///     A deactivated connection takes no part in the traffic: no gateway is built for it. Its key is its own, because
    ///     <c>NotActive</c> is the key whose text says the store is not active.
    /// </summary>
    public static readonly Error NotActive = new() { Key = "WarehouseConnectionNotActive" };

    /// <summary>
    ///     A connection of the in-memory Dummy provider in a Production host: no gateway is built for it, because it would answer
    ///     every release and stock push from memory while the warehouse it stands for never hears of them. Creating such a
    ///     connection is refused already; this covers a row that got into the database some other way.
    /// </summary>
    public static readonly Error DummyProviderNotAllowedInProduction = new()
    {
        Key = "WarehouseDummyProviderNotAllowedInProduction",
        StatusCode = HttpStatusCode.Conflict
    };

    /// <summary>
    ///     The store is not an active store of the caller, as far as Inventory's own store access copy knows: someone else's, not
    ///     active, or never heard of. One answer for all three, so an id does not tell a stranger whether the store exists.
    /// </summary>
    public static readonly Error StoreNotAccessible = new() { Key = nameof(StoreNotAccessible), StatusCode = HttpStatusCode.NotFound };
}
