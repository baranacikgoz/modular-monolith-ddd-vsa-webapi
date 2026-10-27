using Common.Domain.StronglyTypedIds;

namespace Common.IntegrationEvents;

public sealed record StoreCreatedIntegrationEvent(DefaultIdType StoreId, ApplicationUserId OwnerId) : IntegrationEvent;

public sealed record ProductCreatedIntegrationEvent(DefaultIdType ProductId, string Name, string Description, int Quantity)
    : IntegrationEvent;

/// <summary>
///     The facts a read model in another module keeps about a product. <paramref name="Version" /> is the Product
///     aggregate version, so a consumer applies it through <c>UpsertIfNewerAsync</c> and drops a late or redelivered
///     event as stale.
/// </summary>
public sealed record ProductChangedIntegrationEvent(DefaultIdType ProductId, DefaultIdType StoreId, string Name, long Version)
    : IntegrationEvent;

/// <summary>A store passed the approval step; the owner may now act as a store owner.</summary>
public sealed record StoreApprovedIntegrationEvent(DefaultIdType StoreId, ApplicationUserId OwnerId) : IntegrationEvent;

/// <summary>Queue message of one product import job. Carries only the job id: the job row holds everything else, and a
/// message for a job that is no longer Queued is a no-op.</summary>
public sealed record ProductImportRequestedIntegrationEvent(DefaultIdType JobId) : IntegrationEvent;

/// <summary>Queue message of one product export job, same contract as <see cref="ProductImportRequestedIntegrationEvent" />.</summary>
public sealed record ProductExportRequestedIntegrationEvent(DefaultIdType JobId) : IntegrationEvent;
