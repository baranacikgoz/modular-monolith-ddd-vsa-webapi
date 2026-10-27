namespace Inventory.Infrastructure.RateLimiting;

public static class Constants
{
    public const string WarehouseWebhook = nameof(WarehouseWebhook);

    /// <summary>The route value the webhook callback route carries the connection id in. The route template spells the same name, and the policy partitions on it.</summary>
    public const string WebhookConnectionIdRouteKey = "connectionId";
}
