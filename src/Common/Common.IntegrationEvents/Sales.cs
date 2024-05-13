using Common.Domain.StronglyTypedIds;

namespace Common.IntegrationEvents;
public sealed record StoreCreatedIntegrationEvent(Guid StoreId, ApplicationUserId OwnerId) : IntegrationEvent;
public sealed record ProductCreatedIntegrationEvent(Guid ProductId, string Name, string Description) : IntegrationEvent;
