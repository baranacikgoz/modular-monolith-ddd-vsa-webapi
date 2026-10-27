using Common.Domain.Events;

namespace Products.Domain.Products.DomainEvents.v1;

public sealed record V1ProductImageAddedDomainEvent(ProductId ProductId, ProductImageId ImageId, string ObjectKey) : DomainEvent;
