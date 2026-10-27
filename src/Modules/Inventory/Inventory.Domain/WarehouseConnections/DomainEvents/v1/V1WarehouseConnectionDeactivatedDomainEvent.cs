using Common.Domain.Events;

namespace Inventory.Domain.WarehouseConnections.DomainEvents.v1;

public sealed record V1WarehouseConnectionDeactivatedDomainEvent(WarehouseConnectionId WarehouseConnectionId) : DomainEvent;
