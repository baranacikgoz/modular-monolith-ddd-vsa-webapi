using Inventory.Domain.WarehouseConnections;

namespace Inventory.Endpoints.WarehouseConnections.v1.Create;

/// <summary>
///     The only response of the create call, and (with the rotate call) the only place a webhook secret is ever shown. The
///     caller must store <see cref="WebhookSecret" /> now and give it to the warehouse system together with the callback
///     address; it cannot be read back. The API credential is never echoed.
/// </summary>
public sealed record Response
{
    public required WarehouseConnectionId Id { get; init; }
    public required string WebhookSecret { get; init; }

    /// <summary>Path of the callback route the warehouse system posts its signed webhooks to.</summary>
    public required string WebhookPath { get; init; }
}
