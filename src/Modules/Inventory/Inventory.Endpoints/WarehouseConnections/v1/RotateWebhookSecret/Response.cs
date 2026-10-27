namespace Inventory.Endpoints.WarehouseConnections.v1.RotateWebhookSecret;

/// <summary>
///     Shows the new webhook secret once, like the create call does. The previous secret stops verifying as soon as this
///     response is produced, so the warehouse system must be given the new one right away.
/// </summary>
public sealed record Response
{
    public required string WebhookSecret { get; init; }

    /// <summary>Path of the callback route the warehouse system posts its signed webhooks to.</summary>
    public required string WebhookPath { get; init; }
}
