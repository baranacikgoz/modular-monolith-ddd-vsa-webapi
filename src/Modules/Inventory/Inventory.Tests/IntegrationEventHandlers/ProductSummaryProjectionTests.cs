using Common.IntegrationEvents;
using Common.Tests;
using Inventory.Application.IntegrationEventHandlers;
using Inventory.Application.Persistence;
using Inventory.Application.ProductSummaries;
using Inventory.Tests.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Inventory.Tests.IntegrationEventHandlers;

/// <summary>
///     Products' <c>ProductChangedIntegrationEvent</c> feeds Inventory's local product copy, driven through the
///     consumer's own <c>Consume</c> against the real Inventory schema.
/// </summary>
[Collection("IntegrationTestCollection")]
public class ProductSummaryProjectionTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private static readonly DefaultIdType StoreId = DefaultIdType.CreateVersion7();

    private Task DeliverAsync(ProductChangedIntegrationEvent @event)
    {
        return ConsumerHarness.DeliverAsync<UpdateProductSummaryOnProductChangedHandler, ProductChangedIntegrationEvent>(
            Factory.Services, @event);
    }

    private async Task<ProductSummary> RowAsync(DefaultIdType productId)
    {
        await using var scope = Factory.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        return await db.ProductSummaries.AsNoTracking().SingleAsync(p => p.ProductId == productId);
    }

    [Fact]
    public async Task Consume_UnknownProduct_CreatesTheRow()
    {
        var productId = DefaultIdType.CreateVersion7();

        await DeliverAsync(new ProductChangedIntegrationEvent(productId, StoreId, "Widget", 1));

        var row = await RowAsync(productId);
        Assert.Equal("Widget", row.Name);
        Assert.Equal(StoreId, row.StoreId);
        Assert.Equal(1, row.SourceVersion);
    }

    [Fact]
    public async Task Consume_NewerVersion_UpdatesTheName()
    {
        var productId = DefaultIdType.CreateVersion7();
        await DeliverAsync(new ProductChangedIntegrationEvent(productId, StoreId, "Widget", 1));

        await DeliverAsync(new ProductChangedIntegrationEvent(productId, StoreId, "Gadget", 2));

        var row = await RowAsync(productId);
        Assert.Equal("Gadget", row.Name);
        Assert.Equal(2, row.SourceVersion);
    }

    /// <summary>Delivery order is never guaranteed: a late version 2 must not undo version 3.</summary>
    [Fact]
    public async Task Consume_StaleVersion_LeavesTheRowUntouched()
    {
        var productId = DefaultIdType.CreateVersion7();
        await DeliverAsync(new ProductChangedIntegrationEvent(productId, StoreId, "Current", 3));

        await DeliverAsync(new ProductChangedIntegrationEvent(productId, StoreId, "Stale", 2));

        var row = await RowAsync(productId);
        Assert.Equal("Current", row.Name);
        Assert.Equal(3, row.SourceVersion);
    }
}
