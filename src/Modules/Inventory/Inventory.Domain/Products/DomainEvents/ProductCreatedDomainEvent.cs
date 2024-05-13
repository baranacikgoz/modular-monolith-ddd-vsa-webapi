using Common.Domain.Events;
using Inventory.Domain.Stores;

namespace Inventory.Domain.Products.DomainEvents;
public sealed record ProductCreatedDomainEvent(ProductId Id, string Name, string Description) : DomainEvent;
