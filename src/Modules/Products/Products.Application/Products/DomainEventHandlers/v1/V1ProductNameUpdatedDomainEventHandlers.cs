using Common.Application.EventBus;
using Common.IntegrationEvents;
using Microsoft.EntityFrameworkCore;
using Products.Application.Persistence;
using Products.Domain.Products;
using Products.Domain.Products.DomainEvents.v1;

namespace Products.Application.Products.DomainEventHandlers.v1;

public static class V1ProductNameUpdatedDomainEventHandlers
{
    /// <summary>
    ///     Domain events are dispatched from <c>SaveChanges</c> before anything is written, so the product is in the
    ///     change tracker with its new state; the keyed read is only the fallback for a product the tracker does not
    ///     hold. The version is the domain event's own, never the entity's: a save that raises several events must not
    ///     stamp the first one with a later version.
    /// </summary>
    public class ProductChangedIntegrationEventPublishingHandler(IIntegrationEventOutbox outbox, IProductsDbContext dbContext)
        : DomainEventHandlerBase<V1ProductNameUpdatedDomainEvent>
    {
        public override async Task HandleAsync(V1ProductNameUpdatedDomainEvent @event, CancellationToken cancellationToken)
        {
            var tracked = dbContext.ChangeTracker.Entries<Product>().FirstOrDefault(e => e.Entity.Id == @event.ProductId)?.Entity;

            var storeId = tracked is not null
                ? tracked.StoreId
                : await dbContext.Products
                    .AsNoTracking()
                    .TagWith(nameof(HandleAsync))
                    .Where(p => p.Id == @event.ProductId)
                    .Select(p => p.StoreId)
                    .SingleAsync(cancellationToken);

            outbox.Collect(new ProductChangedIntegrationEvent(
                @event.ProductId.Value, storeId.Value, @event.Name, @event.Version));
        }
    }
}
