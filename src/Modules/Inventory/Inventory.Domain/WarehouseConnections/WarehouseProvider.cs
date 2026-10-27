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
    FormApi,

    /// <summary>
    ///     An in-memory stand-in with no network call, no base address and no credential, for local development and tests.
    ///     It answers like the real providers do and keeps what it was sent for the life of the process. Refused when a
    ///     connection is created in Production.
    /// </summary>
    Dummy
}
