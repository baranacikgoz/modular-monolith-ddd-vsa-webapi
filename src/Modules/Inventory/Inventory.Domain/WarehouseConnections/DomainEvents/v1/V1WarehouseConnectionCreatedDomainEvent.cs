using Common.Domain.Events;

namespace Inventory.Domain.WarehouseConnections.DomainEvents.v1;

/// <summary>Carries no credential and no webhook secret: the audit log keeps events for years.</summary>
public sealed record V1WarehouseConnectionCreatedDomainEvent(
    WarehouseConnectionId WarehouseConnectionId,
    DefaultIdType StoreId,
    V1WarehouseConnectionCreatedDomainEvent.ProviderSnapshot Provider,
    Uri BaseUrl) : DomainEvent
{
    public enum ProviderSnapshot
    {
        JsonApi,
        FormApi
    }
}

internal static class V1WarehouseConnectionCreatedDomainEventExtensions
{
    public static V1WarehouseConnectionCreatedDomainEvent.ProviderSnapshot ToCreatedSnapshot(this WarehouseProvider provider)
    {
        return provider switch
        {
            WarehouseProvider.JsonApi => V1WarehouseConnectionCreatedDomainEvent.ProviderSnapshot.JsonApi,
            WarehouseProvider.FormApi => V1WarehouseConnectionCreatedDomainEvent.ProviderSnapshot.FormApi,
            _ => throw new ArgumentOutOfRangeException(nameof(provider), provider, null)
        };
    }
}
