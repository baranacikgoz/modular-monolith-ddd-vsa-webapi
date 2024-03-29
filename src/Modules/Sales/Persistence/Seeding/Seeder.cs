using Common.InterModuleRequests.IdentityAndAuth;
using MassTransit;

namespace Sales.Persistence.Seeding;

internal sealed partial class Seeder(
    IRequestClient<FirstBasicUserIdsRequest> requestClient,
    SalesDbContext dbContext
    )
{
    private const int StoreCount = 2;
    public async Task SeedDbAsync(CancellationToken cancellationToken = default)
    {
        await SeedStoresAsync(cancellationToken);
        await SeedProductsAsync(cancellationToken);
    }
}
