using Common.Application.Options;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Products;
using Common.Tests;
using Inventory.Application.Persistence;
using Inventory.Application.ProductSummaries;
using Inventory.Infrastructure.ProductSummaries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;

namespace Inventory.Tests.ProductSummaries;

/// <summary>
///     Nightly heal of the product copy from Products, run directly with a substituted Products client: the job body
///     is what matters, Hangfire only fires it.
/// </summary>
[Collection("IntegrationTestCollection")]
public class ProductSummariesReconciliationJobTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private static readonly DefaultIdType StoreId = DefaultIdType.CreateVersion7();

    private IInventoryDbContext Db => Scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();

    private ProductSummariesReconciliationJob Job(IInterModuleRequestClient<GetProductsPageRequest, GetProductsPageResponse> client)
    {
        return new ProductSummariesReconciliationJob(
            Db,
            client,
            Options.Create(new ProjectionReconciliationOptions { PageSize = 2, MaxPages = 10 }),
            NullLogger<ProductSummariesReconciliationJob>.Instance);
    }

    private static ProductPageItem Source(string name, long version)
    {
        return new ProductPageItem(DefaultIdType.CreateVersion7(), StoreId, name, version);
    }

    private static IInterModuleRequestClient<GetProductsPageRequest, GetProductsPageResponse> ClientReturning(
        params GetProductsPageResponse[] pages)
    {
        var client = Substitute.For<IInterModuleRequestClient<GetProductsPageRequest, GetProductsPageResponse>>();
        client.SendAsync(Arg.Any<GetProductsPageRequest>(), Arg.Any<CancellationToken>()).Returns(pages[0], pages[1..]);
        return client;
    }

    private async Task SeedRowAsync(ProductPageItem source, string name, long version)
    {
        var row = ProductSummary.Create(source.ProductId);
        row.ApplyIfNewer(source.StoreId, name, version);
        Db.ProductSummaries.Add(row);
        await Db.SaveChangesAsync();
        Db.ChangeTracker.Clear();
    }

    private async Task<ProductSummary> RowAsync(DefaultIdType productId)
    {
        return await Db.ProductSummaries.AsNoTracking().SingleAsync(p => p.ProductId == productId);
    }

    [Fact]
    public async Task RunAsync_ProductWithoutRow_CreatesIt()
    {
        var source = Source("Widget", 4);
        var client = ClientReturning(new GetProductsPageResponse([source], null));

        await Job(client).RunAsync();

        var row = await RowAsync(source.ProductId);
        Assert.Equal("Widget", row.Name);
        Assert.Equal(StoreId, row.StoreId);
        Assert.Equal(4, row.SourceVersion);
    }

    [Fact]
    public async Task RunAsync_SourceNewerThanRow_CorrectsTheName()
    {
        var source = Source("Gadget", 5);
        await SeedRowAsync(source, "Widget", version: 3);
        var client = ClientReturning(new GetProductsPageResponse([source], null));

        await Job(client).RunAsync();

        var row = await RowAsync(source.ProductId);
        Assert.Equal("Gadget", row.Name);
        Assert.Equal(5, row.SourceVersion);
    }

    /// <summary>
    ///     The run reads Products over several requests; an event that landed in between is newer than the page it
    ///     read, and must survive the run.
    /// </summary>
    [Fact]
    public async Task RunAsync_SourceOlderThanRow_LeavesTheRowUntouched()
    {
        var source = Source("Stale", 2);
        await SeedRowAsync(source, "Current", version: 6);
        var client = ClientReturning(new GetProductsPageResponse([source], null));

        await Job(client).RunAsync();

        var row = await RowAsync(source.ProductId);
        Assert.Equal("Current", row.Name);
        Assert.Equal(6, row.SourceVersion);
    }

    [Fact]
    public async Task RunAsync_TwoPages_SendsTheFirstPagesLastIdAsTheNextCursor()
    {
        var first = Source("One", 1);
        var second = Source("Two", 1);
        var third = Source("Three", 1);
        var client = ClientReturning(
            new GetProductsPageResponse([first, second], second.ProductId),
            new GetProductsPageResponse([third], null));

        await Job(client).RunAsync();

        await client.Received(1).SendAsync(
            Arg.Is<GetProductsPageRequest>(r => r.AfterProductId == second.ProductId), Arg.Any<CancellationToken>());
        Assert.Equal("Three", (await RowAsync(third.ProductId)).Name);
        Assert.Equal("One", (await RowAsync(first.ProductId)).Name);
    }
}
