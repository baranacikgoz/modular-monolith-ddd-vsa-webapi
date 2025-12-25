using Common.Domain.Events;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;

namespace Products.Domain.Products.DomainEvents.v1;

public sealed record V1ProductCreatedDomainEvent(
    ProductId ProductId,
    StoreId StoreId,
    ProductTemplateId ProductTemplateId,
    string Name,
    string Description,
    int Quantity,
    decimal Price) : DomainEvent;
