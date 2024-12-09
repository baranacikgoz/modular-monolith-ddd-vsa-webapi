using Common.Domain.StronglyTypedIds;
using Microsoft.EntityFrameworkCore;
using Products.Domain.Stores;

namespace Products.Infrastructure.Persistence.Seeding;
internal sealed partial class Seeder
{
    private async Task<List<StoreId>> SeedStoresAsync(CancellationToken cancellationToken)
    {
        var getSeedUserIdsResponse = await requestClient.SendAsync(new(StoreCount), cancellationToken);
        var userIds = getSeedUserIdsResponse.UserIds;

        var storeIds = new List<StoreId>(StoreCount);

        var user1Id = userIds.ElementAt(0);
        var store1Name = "Store 1";
        storeIds.Add(await SeedStore(user1Id, store1Name, cancellationToken));

        var user2Id = userIds.ElementAt(1);
        var store2Name = "Store 2";
        storeIds.Add(await SeedStore(user2Id, store2Name, cancellationToken));

        return storeIds;
    }

    private async Task<StoreId> SeedStore(ApplicationUserId userId, string storeName, CancellationToken cancellationToken)
    {
        if (await dbContext.Stores.SingleOrDefaultAsync(store => store.OwnerId == userId || store.Name == storeName, cancellationToken)
            is not Store store)
        {
            store = Store.Create(userId, storeName, "Seeded by system.");

            await dbContext.Stores.AddAsync(store, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        return store.Id;
    }
}
