using Common.Domain.StronglyTypedIds;

namespace Common.IntegrationEvents;

/// <summary>
///     Who owns a store and whether the store is open for its owner to act on, for a module that decides access on a request
///     path without asking the store's module. <paramref name="IsActive" /> is true only while the store is Active; a
///     store that is pending approval, suspended or deactivated is not. <paramref name="Version" /> is the Store aggregate
///     version of the domain event that raised this one, so a consumer applies it through <c>UpsertIfNewerAsync</c> and drops a
///     late or redelivered event as stale.
/// </summary>
public sealed record StoreAccessChangedIntegrationEvent(DefaultIdType StoreId, ApplicationUserId OwnerId, bool IsActive, long Version)
    : IntegrationEvent;
