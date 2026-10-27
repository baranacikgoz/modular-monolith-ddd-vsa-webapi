using Common.Application.Options;
using Microsoft.Extensions.Options;

namespace Products.Tests.Support;

/// <summary>Private copies of the options a test needs to bend (a tiny batch size, a cache of zero seconds), so no test waits
/// out a production value or changes the options the running host shares.</summary>
internal static class TestOptions
{
    public static IOptions<ProductsOptions> Products(Action<ProductsOptions>? configure = null)
    {
        var options = new ProductsOptions
        {
            ImportJobConcurrentMessageLimit = 2,
            ImportBatchesPerMessage = 10,
            ImportMaxStoredRowErrors = 200,
            ExportJobConcurrentMessageLimit = 2,
            ExportPageSize = 500,
            ExportMaxRows = 100000,
            JobHousekeepingCron = "0 20 * * * *",
            ImageMaxFileBytes = 5242880,
            MaxImagesPerProduct = 8,
            ProductsPageMaxSize = 500,
            SearchCacheSeconds = 60
        };
        configure?.Invoke(options);
        return Options.Create(options);
    }

    public static IOptions<BulkImportOptions> BulkImport(Action<BulkImportOptions>? configure = null)
    {
        var options = new BulkImportOptions { MaxFileBytes = 10485760, MaxRows = 50000, SaveChunkSize = 200 };
        configure?.Invoke(options);
        return Options.Create(options);
    }

    public static IOptions<FullTextSearchOptions> FullTextSearch(Action<FullTextSearchOptions>? configure = null)
    {
        var options = new FullTextSearchOptions
        {
            DefaultConfig = "simple_unaccent",
            UseUnaccent = true,
            CultureToConfig = new Dictionary<string, string> { ["tr"] = "turkish", ["en"] = "english" },
            RankWeights = [0.1f, 0.2f, 0.4f, 1.0f],
            MaxMatchResults = 200
        };
        configure?.Invoke(options);
        return Options.Create(options);
    }
}
