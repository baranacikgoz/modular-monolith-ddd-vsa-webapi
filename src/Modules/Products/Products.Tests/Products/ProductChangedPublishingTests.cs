using Common.IntegrationEvents;
using Microsoft.EntityFrameworkCore;
using Products.Domain.Products;
using Products.Tests.Support;
using Xunit;

namespace Products.Tests.Products;

public class ProductChangedPublishingTests(IntegrationTestWebAppFactory factory) : ProductsIntegrationTest(factory)
{
    [Fact]
    public async Task CreatingAProduct_QueuesProductChangedWithTheCreationVersion()
    {
        var seeded = await ProductsSeed.StoreWithProductsAsync(Db, DefaultUser, "Desk lamp");
        var product = seeded.Products[0];

        var changed = (await OutboxEventsAsync()).OfType<ProductChangedIntegrationEvent>().Single(e => e.ProductId == product.Id.Value);

        Assert.Equal(seeded.Store.Id.Value, changed.StoreId);
        Assert.Equal("Desk lamp", changed.Name);
        Assert.Equal(1, changed.Version);
    }

    [Fact]
    public async Task RenamingAProduct_QueuesProductChangedWithTheEventVersionNotTheLaterOne()
    {
        var seeded = await ProductsSeed.StoreWithProductsAsync(Db, DefaultUser, "Desk lamp");
        var productId = seeded.Products[0].Id;
        Db.ChangeTracker.Clear();

        // The rename (version 2) and the price change (version 3) are raised by one save: the name event must carry 2.
        var product = await Db.Products.SingleAsync(p => p.Id == productId);
        product.Update("Floor lamp", null, null, 99m);
        await Db.SaveChangesAsync();

        var changes = (await OutboxEventsAsync()).OfType<ProductChangedIntegrationEvent>()
            .Where(e => e.ProductId == productId.Value)
            .OrderBy(e => e.Version)
            .ToList();
        Assert.Equal([1L, 2L], changes.Select(e => e.Version));
        Assert.Equal("Floor lamp", changes[1].Name);
        Assert.Equal(seeded.Store.Id.Value, changes[1].StoreId);
    }

    [Fact]
    public async Task ChangingOnlyThePrice_QueuesNoProductChanged()
    {
        var seeded = await ProductsSeed.StoreWithProductsAsync(Db, DefaultUser, "Desk lamp");
        var productId = seeded.Products[0].Id;
        Db.ChangeTracker.Clear();

        var product = await Db.Products.SingleAsync(p => p.Id == productId);
        product.Update(null, null, null, 99m);
        await Db.SaveChangesAsync();

        var changes = (await OutboxEventsAsync()).OfType<ProductChangedIntegrationEvent>().Where(e => e.ProductId == productId.Value);
        Assert.Single(changes);
    }
}
