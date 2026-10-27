using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Products.Infrastructure.InterModuleRequestHandlers;
using Products.Tests.Support;
using Xunit;

namespace Products.Tests.InterModuleRequestHandlers;

public class GetProductsPageRequestHandlerTests(IntegrationTestWebAppFactory factory) : ProductsIntegrationTest(factory)
{
    [Fact]
    public async Task SendAsync_WalkingTheCursor_VisitsEveryProductOnceNewestFirst()
    {
        var seeded = await ProductsSeed.StoreWithProductsAsync(Db, DefaultUser, "P1", "P2", "P3", "P4", "P5");
        // Ids created in the same millisecond have no creation order, so "newest first" is Postgres' own descending id order.
        var expected = (await Db.Products.AsNoTracking().OrderByDescending(p => p.Id).Select(p => p.Id).ToListAsync())
            .Select(id => id.Value).ToList();
        var client = Scope.ServiceProvider.GetRequiredService<IInterModuleRequestClient<GetProductsPageRequest, GetProductsPageResponse>>();

        var seen = new List<ProductPageItem>();
        DefaultIdType? after = null;
        var pages = 0;
        do
        {
            var page = await client.SendAsync(new GetProductsPageRequest(after, 2), CancellationToken.None);
            seen.AddRange(page.Products);
            after = page.NextAfterProductId;
            pages++;
        } while (after is not null);

        Assert.Equal(3, pages);
        Assert.Equal(expected, seen.Select(p => p.ProductId));
        Assert.All(seen, p => Assert.Equal(seeded.Store.Id.Value, p.StoreId));
        Assert.All(seen, p => Assert.Equal(1, p.Version));
    }

    [Fact]
    public async Task HandleAsync_PageSizeAboveTheConfiguredCap_ReturnsTheCappedPage()
    {
        await ProductsSeed.StoreWithProductsAsync(Db, DefaultUser, "P1", "P2", "P3");
        var handler = new GetProductsPageRequestHandler(Db, TestOptions.Products(o => o.ProductsPageMaxSize = 2));

        var page = await handler.HandleAsync(new GetProductsPageRequest(null, 100), CancellationToken.None);

        Assert.Equal(2, page.Products.Count);
        Assert.Equal(page.Products[^1].ProductId, page.NextAfterProductId);
    }
}
