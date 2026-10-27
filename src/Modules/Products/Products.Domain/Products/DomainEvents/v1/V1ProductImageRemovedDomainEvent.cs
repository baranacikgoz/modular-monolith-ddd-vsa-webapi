using Common.Domain.Events;

namespace Products.Domain.Products.DomainEvents.v1;

public sealed record V1ProductImageRemovedDomainEvent(ProductId ProductId, ProductImageId ImageId, string ObjectKey) : DomainEvent;
