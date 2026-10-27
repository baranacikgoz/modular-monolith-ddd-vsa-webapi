using Common.Domain.Events;

namespace Products.Domain.Products.DomainEvents.v1;

public sealed record V1ProductSkuAssignedDomainEvent(ProductId ProductId, string Sku) : DomainEvent;
