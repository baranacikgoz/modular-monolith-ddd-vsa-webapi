namespace Sales.Persistence.Seeding;
using Common.InterModuleRequests.IdentityAndAuth;
using Sales.Features.Stores.Domain;

internal sealed partial class Seeder
{
    private async Task SeedStoresAsync(CancellationToken cancellationToken)
    {
        var basicUserIdResponse = await requestClient.GetResponse<FirstBasicUserIdResponse>(new(), cancellationToken);
        var basicUserId = basicUserIdResponse.Message.UserId;

        var store = Store.Create(
            ownerId: basicUserId,
            name: "Store 1");

        await dbContext.Stores.AddAsync(store, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
