using Common.Domain.Events;

namespace Inventory.Domain.WarehouseConnections.DomainEvents.v1;

/// <summary>
///     The stored secrets of a connection were written again under a newer encryption key. The secrets themselves did not
///     change; the flags say which of the two stored values was rewritten. Never carries a value.
/// </summary>
public sealed record V1WarehouseConnectionSecretsReprotectedDomainEvent(
    WarehouseConnectionId WarehouseConnectionId, bool ApiCredential, bool WebhookSecret) : DomainEvent;
