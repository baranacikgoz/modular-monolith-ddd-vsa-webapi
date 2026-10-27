namespace Inventory.Domain.WarehouseConnections;

/// <summary>Stored as its name. Deactivated is final: a store connects again by creating a new connection.</summary>
public enum WarehouseConnectionStatus
{
    Active,
    Deactivated
}
