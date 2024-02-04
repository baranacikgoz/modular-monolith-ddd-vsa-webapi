using Common.EventBus.Contracts;
using Common.InterModuleRequests.IdentityAndAuth;
using MassTransit;
using Microsoft.Extensions.Hosting;
using NimbleMediator.Contracts;

namespace Sales.Persistence.Seeding;

internal sealed partial class Seeder(
    IRequestClient<FirstBasicUserIdRequest> requestClient,
    SalesDbContext dbContext
    )
{
    public async Task SeedDbAsync(CancellationToken cancellationToken = default)
    {
        await SeedStoresAsync(cancellationToken);
        await SeedProductsAsync(cancellationToken);
    }
}
