using Common.Domain.ResultMonad;

namespace Inventory.Domain.WarehouseConnections.Errors;

public static class WarehouseConnectionErrors
{
    public static readonly Error CannotChangeWhileDeactivated = new() { Key = nameof(CannotChangeWhileDeactivated) };
}
