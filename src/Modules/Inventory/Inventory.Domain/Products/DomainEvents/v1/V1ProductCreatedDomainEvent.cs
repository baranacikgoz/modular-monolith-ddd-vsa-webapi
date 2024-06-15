using Common.Domain.Events;
using Inventory.Domain.Stores;

namespace Inventory.Domain.Products.DomainEvents.v1;
public sealed record V1ProductCreatedDomainEvent(ProductId Id, string Name, string Description) : DomainEvent;
