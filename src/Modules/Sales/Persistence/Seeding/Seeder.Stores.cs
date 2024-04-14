namespace Sales.Persistence.Seeding;

using Common.InterModuleRequests.IdentityAndAuth;
using Microsoft.EntityFrameworkCore;
using Sales.Features.Stores.Domain;

internal sealed partial class Seeder
{
    private async Task SeedStoresAsync(CancellationToken cancellationToken)
    {
        var getSeedUserIdsResponse = await requestClient.SendAsync(new(StoreCount), cancellationToken);
        var userIds = getSeedUserIdsResponse.UserIds;

        var user1Id = userIds.ElementAt(0);
        var store1Name = "Store 1";
        await SeedStore(user1Id, store1Name, cancellationToken);

        var user2Id = userIds.ElementAt(1);
        var store2Name = "Store 2";
        await SeedStore(user2Id, store2Name, cancellationToken);
    }

    private async Task SeedStore(Guid userId, string storeName, CancellationToken cancellationToken)
    {
        if (await dbContext.Stores.AnyAsync(store => store.OwnerId == userId || store.Name == storeName, cancellationToken))
        {
            return;
        }

        var store1 = Store.Create(userId, storeName);

        await dbContext.Stores.AddAsync(store1, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
