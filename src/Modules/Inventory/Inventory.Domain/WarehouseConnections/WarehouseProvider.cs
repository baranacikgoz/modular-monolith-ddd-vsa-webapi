namespace Inventory.Domain.WarehouseConnections;

/// <summary>
///     The warehouse system a connection talks to: one gateway implementation per member. Stored as its name, so a member
///     is never renamed or reordered away.
/// </summary>
public enum WarehouseProvider
{
    /// <summary>JSON request bodies, bearer token authentication.</summary>
    JsonApi,

    /// <summary>Form encoded request bodies, API key header authentication.</summary>
    FormApi
}
