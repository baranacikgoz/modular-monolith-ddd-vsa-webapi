using Common.Domain.StronglyTypedIds;

namespace Common.IntegrationEvents;
public sealed record StoreCreatedIntegrationEvent(DefaultIdType StoreId, ApplicationUserId OwnerId) : IntegrationEvent;
public sealed record ProductCreatedIntegrationEvent(DefaultIdType ProductId, string Name, string Description) : IntegrationEvent;
