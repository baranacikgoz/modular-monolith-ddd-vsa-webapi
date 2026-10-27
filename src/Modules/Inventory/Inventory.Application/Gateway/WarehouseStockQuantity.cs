namespace Inventory.Application.Gateway;

/// <summary>The on-hand quantity one product should show at the warehouse. Absolute, never a delta.</summary>
public sealed record WarehouseStockQuantity(string ProductReference, int Quantity);
