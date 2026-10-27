using System.Collections.Concurrent;
using Common.Application.Caching;
using Common.Application.Options;
using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.IAM;
using IAM.Application.Keycloak;
using Microsoft.Extensions.Options;
using ZiggyCreatures.Caching.Fusion;

namespace IAM.Infrastructure.InterModuleRequestHandlers;

/// <summary>
///     Bulk-by-ids display lookup for other modules (audit "who" columns via <c>ActorNames</c>). Users live in
///     Keycloak and the Admin API has no lookup by id list, so every unknown id costs one <c>GET /users/{id}</c>.
///     Each id is served from FusionCache first (<c>user_display:{id}</c>, L1 minutes, L2 a day: a name changes only
///     from the Keycloak console, and <c>DeleteUserAsync</c> evicts the entry), and only the misses go to Keycloak,
///     at most <see cref="KeycloakOptions.UserLookupConcurrency" /> in flight.
///     An id Keycloak does not know is cached as a negative entry so a page listing a deleted actor does not re-ask
///     on every refresh. Fail-safe is on: while Keycloak is down a stale name beats an empty column.
/// </summary>
public class GetUsersRequestHandler(
    IKeycloakAdminClient adminClient,
    IFusionCache cache,
    IOptions<KeycloakOptions> keycloakOptions)
    : InterModuleRequestHandler<GetUsersRequest, GetUsersResponse>
{
    public override async Task<GetUsersResponse> HandleAsync(GetUsersRequest request, CancellationToken cancellationToken)
    {
        var options = keycloakOptions.Value;
        var ids = request.UserIds.Distinct().ToList();
        var entries = new ConcurrentDictionary<ApplicationUserId, UserDisplayCacheEntry>();
        var misses = new List<ApplicationUserId>();

        foreach (var id in ids)
        {
            var cached = await cache.TryGetAsync<UserDisplayCacheEntry>(CacheKeys.For.UserDisplay(id), token: cancellationToken);
            if (cached.HasValue)
            {
                entries[id] = cached.Value;
            }
            else
            {
                misses.Add(id);
            }
        }

        await Parallel.ForEachAsync(
            misses,
            new ParallelOptions { MaxDegreeOfParallelism = options.UserLookupConcurrency, CancellationToken = cancellationToken },
            async (id, ct) => entries[id] = await FetchAndCacheAsync(id, options, ct));

        var users = ids
            .Select(id => (Id: id, Entry: entries[id]))
            .Where(x => x.Entry.Exists)
            .Select(x => new UserSummary(x.Id, x.Entry.Email, x.Entry.FirstName, x.Entry.LastName))
            .ToList();

        return new GetUsersResponse(users);
    }

    private ValueTask<UserDisplayCacheEntry> FetchAndCacheAsync(
        ApplicationUserId id, KeycloakOptions options, CancellationToken cancellationToken)
    {
        // A fresh options instance on purpose (same reason as IntegrationEventHandlerBase): the configured defaults
        // carry factory timeouts that would turn a slow Keycloak into a synthetic timeout mid-lookup.
        var entryOptions = new FusionCacheEntryOptions
        {
            Duration = TimeSpan.FromSeconds(options.UserDisplayCacheDurationSeconds),
            DistributedCacheDuration = TimeSpan.FromSeconds(options.UserDisplayCacheDistributedDurationSeconds),
            IsFailSafeEnabled = true
        };

        return cache.GetOrSetAsync<UserDisplayCacheEntry>(
            CacheKeys.For.UserDisplay(id),
            async (ctx, ct) =>
            {
                // GetUserAsync's only Result failure is NotFound; transport and 5xx failures throw, and with fail-safe
                // on FusionCache then serves the stale entry if it has one instead of surfacing the exception.
                var result = await adminClient.GetUserAsync(id, ct);
                if (result.Value is { } user)
                {
                    return new UserDisplayCacheEntry(true, user.Email, user.FirstName, user.LastName);
                }

                var negativeDuration = TimeSpan.FromSeconds(options.UserDisplayNegativeCacheDurationSeconds);
                ctx.Options.Duration = negativeDuration;
                ctx.Options.DistributedCacheDuration = negativeDuration;
                return UserDisplayCacheEntry.Missing;
            },
            options: entryOptions,
            token: cancellationToken);
    }
}

/// <summary>
///     What the bulk lookup keeps per user: only the display fields <see cref="UserSummary" /> exposes. The id is the
///     cache key, not part of the value. <see cref="Exists" /> false is the negative entry for an id Keycloak does not
///     know. Public so the L2 (Redis) serializer can round-trip it.
/// </summary>
public sealed record UserDisplayCacheEntry(bool Exists, string? Email, string? FirstName, string? LastName)
{
    public static readonly UserDisplayCacheEntry Missing = new(false, null, null, null);
}
