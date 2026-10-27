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
}
