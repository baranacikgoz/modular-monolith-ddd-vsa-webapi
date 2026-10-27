using System.Net;
using Common.Application.Auth;
using Common.Application.EventBus;
using Common.Application.Options;
using Common.Application.Persistence.Inbox;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using Common.IntegrationEvents;
using IAM.Application.Keycloak;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ZiggyCreatures.Caching.Fusion;

namespace IAM.Infrastructure.IntegrationEventHandlers;

/// <summary>
///     Grants the "store owner" realm role once a store passes approval: the Products module does not ask IAM
///     synchronously, the role follows the approval through the outbox. Keycloak's role-mapping POST is idempotent
///     (an already-held role is accepted), so a redelivery or a second store of the same owner is harmless on top of
///     the base class's cache pre-filter. IAM owns no database, so it uses the <see cref="StatelessInboxStore" />: no
///     transactional inbox row, idempotency rests on Keycloak's role-mapping semantics. An owner Keycloak no longer
///     knows is logged and skipped (the message must not poison the queue); any other failure is thrown so the bus retries.
/// </summary>
public sealed partial class AssignStoreOwnerRoleOnStoreApprovedHandler(
    IKeycloakAdminClient adminClient,
    IFusionCache cache,
    IOptions<CachingOptions> cachingOptions,
    StatelessInboxStore inbox,
    ILogger<AssignStoreOwnerRoleOnStoreApprovedHandler> logger
) : IntegrationEventHandlerBase<StoreApprovedIntegrationEvent>(cache, cachingOptions, inbox, logger)
{
    protected override async Task ProcessAsync(StoreApprovedIntegrationEvent @event, CancellationToken cancellationToken)
    {
        var result = await adminClient.AssignRealmRoleAsync(@event.OwnerId, KeycloakRoles.StoreOwner, cancellationToken);

        result.Match(
            onSuccess: () => true,
            onFailure: error => error.StatusCode == HttpStatusCode.NotFound
                ? SkipMissingOwner(@event)
                : throw new InvalidOperationException(
                    $"Keycloak did not accept role '{KeycloakRoles.StoreOwner}' for user {@event.OwnerId} (store {@event.StoreId}): {error.Key}."));
    }

    private bool SkipMissingOwner(StoreApprovedIntegrationEvent @event)
    {
        LogOwnerNotFound(logger, @event.OwnerId, @event.StoreId, KeycloakRoles.StoreOwner);
        return false;
    }

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "Keycloak has no user {UserId} for approved store {StoreId}; role {RoleName} not assigned, event skipped.")]
    private static partial void LogOwnerNotFound(ILogger logger, ApplicationUserId userId, DefaultIdType storeId, string roleName);
}
