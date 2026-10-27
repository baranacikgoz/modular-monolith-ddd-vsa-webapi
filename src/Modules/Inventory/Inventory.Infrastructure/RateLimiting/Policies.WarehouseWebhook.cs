using System.Threading.RateLimiting;
using Common.Application.Options;
using Common.Infrastructure.RateLimiting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;

namespace Inventory.Infrastructure.RateLimiting;

public static partial class Policies
{
    // internal (not private): unit-tested directly from Inventory.Tests via InternalsVisibleTo. One bucket per warehouse
    // connection (the {connectionId} route value), never per IP: a warehouse provider delivers the webhooks of all its
    // customers from the same egress addresses, so an IP bucket lets one store's burst throttle every other store's
    // deliveries. The route value is caller-supplied, so this is fairness between legitimate callers, not abuse protection;
    // the webhook path is deliberately not in CustomRateLimitingOptions.ExemptPathPrefixes, so the global per-IP limiter stays
    // the ceiling against a client that sprays random connection ids.
    internal sealed class WarehouseWebhookRateLimitingPolicy(IOptions<CustomRateLimitingOptions> rateLimitingOptionsProvider)
        : IRateLimiterPolicy<string>
    {
        // Null: falls through to the global OnRejected handler.
        public Func<OnRejectedContext, CancellationToken, ValueTask>? OnRejected => null;

        public RateLimitPartition<string> GetPartition(HttpContext httpContext)
        {
            return RateLimitPartitions.FixedWindowByRouteValue(
                httpContext, Constants.WarehouseWebhook, Constants.WebhookConnectionIdRouteKey, rateLimitingOptionsProvider.Value.WarehouseWebhook);
        }
    }
}
