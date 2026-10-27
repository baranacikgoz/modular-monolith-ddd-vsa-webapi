using Common.Domain.ResultMonad;

namespace Inventory.Domain.WarehouseConnections.Errors;

public static class WarehouseConnectionErrors
{
    public static readonly Error CannotChangeWhileDeactivated = new() { Key = nameof(CannotChangeWhileDeactivated) };

    /// <summary>A deactivated connection takes no part in the traffic: no gateway is built for it.</summary>
    public static readonly Error NotActive = new() { Key = nameof(NotActive) };
}
