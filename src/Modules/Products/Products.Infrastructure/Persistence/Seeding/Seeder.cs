using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.IAM;
using Microsoft.Extensions.Logging;

namespace Products.Infrastructure.Persistence.Seeding;

internal sealed partial class Seeder(
    IInterModuleRequestClient<GetSeedUserIdsRequest, GetSeedUserIdsResponse> requestClient,
    ProductsDbContext dbContext,
    ILogger<Seeder> logger
)
{
    /// <summary>
    ///     Keep <see cref="ProductCount" /> divisible by <see cref="StoreCount" />
    /// </summary>
    private const int StoreCount = 2;

    private const int ProductCount = 4;

    public async Task SeedDbAsync(CancellationToken cancellationToken = default)
    {
        var storeIds = await SeedStoresAsync(cancellationToken);
        var productIds = await SeedProductTemplatesAsync(cancellationToken);

        var storeProductCountPerStore = ProductCount / StoreCount;

        await SeedProductAsync(storeIds, productIds, storeProductCountPerStore, cancellationToken);
    }

    private static partial class LoggerMessages
    {
        [LoggerMessage(Level = LogLevel.Warning, Message = "IAM module not available or request timed out. Skipping store seeding. Error: {ErrorMessage}")]
        public static partial void LogIamModuleNotAvailable(ILogger logger, string errorMessage);
    }
}
