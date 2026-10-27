using System.Net;
using Microsoft.EntityFrameworkCore;
using Products.Domain.Stores;
using Products.Domain.Stores.DomainEvents.v1;
using Products.Tests.Support;
using Xunit;

namespace Products.Tests.Endpoints.Stores;

public class DeactivateTests(IntegrationTestWebAppFactory factory) : ProductsIntegrationTest(factory)
{
    private static Uri Route(StoreId id) => new($"/v1/stores/{id}/deactivate", UriKind.Relative);

    [Fact]
    public async Task Deactivate_StoreHoldingProductsWithStock_RaisesTheStockOnHandEventWithTheCount()
    {
        var seeded = await ProductsSeed.StoreWithProductsAsync(Db, DefaultUser, "Lamp", "Desk");

        using var response = await AuthorizedClient().PostAsync(Route(seeded.Store.Id), content: null);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        var events = await Db.AuditLog.AsNoTracking()
            .Where(e => e.AggregateId == seeded.Store.Id.Value)
            .Select(e => e.Event)
            .ToListAsync();
        var raised = Assert.Single(events.OfType<V1StoreDeactivatedWithStockOnHandDomainEvent>());
        Assert.Equal(2, raised.ProductsWithStockCount);
        Assert.Empty(events.OfType<V1StoreDeactivatedDomainEvent>());
    }

    [Fact]
    public async Task Deactivate_StoreWithoutStock_RaisesThePlainEvent()
    {
        var store = Store.Create(DefaultUser, $"Store {Guid.NewGuid():N}", "Description", "Address");
        Db.Stores.Add(store);
        await Db.SaveChangesAsync();

        using var response = await AuthorizedClient().PostAsync(Route(store.Id), content: null);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        var events = await Db.AuditLog.AsNoTracking()
            .Where(e => e.AggregateId == store.Id.Value)
            .Select(e => e.Event)
            .ToListAsync();
        Assert.Single(events.OfType<V1StoreDeactivatedDomainEvent>());
        Assert.Empty(events.OfType<V1StoreDeactivatedWithStockOnHandDomainEvent>());
    }
}
