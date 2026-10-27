using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Products.Domain.Stores;
using Products.Infrastructure.InterModuleRequestHandlers;
using Products.Tests.Support;
using Xunit;

namespace Products.Tests.InterModuleRequestHandlers;

public class GetStoresPageRequestHandlerTests(IntegrationTestWebAppFactory factory) : ProductsIntegrationTest(factory)
{
    /// <summary>A user owns at most one store.</summary>
    private static ApplicationUserId NewOwner() => new(DefaultIdType.CreateVersion7());

    private async Task<Store> SaveAsync(Store store)
    {
        Db.Stores.Add(store);
        await Db.SaveChangesAsync();
        return store;
    }

    [Fact]
    public async Task SendAsync_WalkingTheCursor_VisitsEveryStoreOnceNewestFirst()
    {
        var owners = new Dictionary<DefaultIdType, ApplicationUserId>();
        for (var i = 0; i < 5; i++)
        {
            var store = await SaveAsync(Store.Create(NewOwner(), $"Store {i}", "Description", "Address"));
            owners[store.Id.Value] = store.OwnerId;
        }

        // Ids created in the same millisecond have no creation order, so "newest first" is Postgres' own descending id order.
        var expected = (await Db.Stores.AsNoTracking().OrderByDescending(s => s.Id).Select(s => s.Id).ToListAsync())
            .Select(id => id.Value).ToList();
        var client = Scope.ServiceProvider.GetRequiredService<IInterModuleRequestClient<GetStoresPageRequest, GetStoresPageResponse>>();

        var seen = new List<StorePageItem>();
        DefaultIdType? after = null;
        var pages = 0;
        do
        {
            var page = await client.SendAsync(new GetStoresPageRequest(after, 2), CancellationToken.None);
            seen.AddRange(page.Stores);
            after = page.NextAfterStoreId;
            pages++;
        } while (after is not null);

        Assert.Equal(3, pages);
        Assert.Equal(expected, seen.Select(s => s.StoreId));
        Assert.All(seen, s => Assert.Equal(owners[s.StoreId], s.OwnerId));
        Assert.All(seen, s => Assert.Equal(1, s.Version));
    }

    /// <summary>The same rule the live events follow: a store is active only while its status is Active.</summary>
    [Fact]
    public async Task HandleAsync_EveryStatus_IsActiveOnlyForAnActiveStoreAndCarriesTheAggregateVersion()
    {
        var active = await SaveAsync(Store.Create(NewOwner(), "Active", "Description", "Address"));
        var pending = await SaveAsync(Store.Apply(NewOwner(), "Pending", "Description", "Address"));
        var suspended = Store.Create(NewOwner(), "Suspended", "Description", "Address");
        suspended.Suspend();
        await SaveAsync(suspended);
        var inactive = Store.Create(NewOwner(), "Inactive", "Description", "Address");
        inactive.Deactivate();
        await SaveAsync(inactive);
        var handler = new GetStoresPageRequestHandler(Db, TestOptions.Products());

        var page = await handler.HandleAsync(new GetStoresPageRequest(null, 100), CancellationToken.None);

        var byId = page.Stores.ToDictionary(s => s.StoreId);
        Assert.True(byId[active.Id.Value].IsActive);
        Assert.False(byId[pending.Id.Value].IsActive);
        Assert.False(byId[suspended.Id.Value].IsActive);
        Assert.False(byId[inactive.Id.Value].IsActive);
        Assert.Equal(2, byId[suspended.Id.Value].Version);
        Assert.Null(page.NextAfterStoreId);
    }

    [Fact]
    public async Task HandleAsync_PageSizeAboveTheConfiguredCap_ReturnsTheCappedPage()
    {
        for (var i = 0; i < 3; i++)
        {
            await SaveAsync(Store.Create(NewOwner(), $"Store {i}", "Description", "Address"));
        }

        var handler = new GetStoresPageRequestHandler(Db, TestOptions.Products(o => o.StoresPageMaxSize = 2));

        var page = await handler.HandleAsync(new GetStoresPageRequest(null, 100), CancellationToken.None);

        Assert.Equal(2, page.Stores.Count);
        Assert.Equal(page.Stores[^1].StoreId, page.NextAfterStoreId);
    }
}
