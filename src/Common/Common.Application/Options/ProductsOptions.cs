using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public class ProductsOptions
{
    /// <summary>Import messages one pod works on at once. Also the consumer's prefetch count: a prefetched message
    /// waiting for a slot sits unacked and would trip the broker's consumer timeout.</summary>
    public required int ImportJobConcurrentMessageLimit { get; set; }

    /// <summary>Batches of <see cref="BulkImportOptions.SaveChunkSize" /> rows one import message saves before the job
    /// stores its cursor and queues a continuation message for itself.</summary>
    public required int ImportBatchesPerMessage { get; set; }

    /// <summary>Row errors kept on an import job; further rejected rows are only counted.</summary>
    public required int ImportMaxStoredRowErrors { get; set; }

    /// <summary>Export messages one pod works on at once. Also the consumer's prefetch count.</summary>
    public required int ExportJobConcurrentMessageLimit { get; set; }

    /// <summary>Products read per keyset page while an export is built.</summary>
    public required int ExportPageSize { get; set; }

    /// <summary>An export of more products than this fails: the file is built in memory.</summary>
    public required int ExportMaxRows { get; set; }

    /// <summary>Cron of the recurring job that fails stale import and export jobs and deletes finished ones.</summary>
    public required string JobHousekeepingCron { get; set; }

    public required long ImageMaxFileBytes { get; set; }

    public required int MaxImagesPerProduct { get; set; }

    /// <summary>Most name and value attributes one product may carry.</summary>
    public required int MaxAttributesPerProduct { get; set; }

    /// <summary>Largest page <c>GetProductsPageRequest</c> returns, whatever size the caller asks for.</summary>
    public required int ProductsPageMaxSize { get; set; }

    /// <summary>How long a <c>SearchProductsByTermRequest</c> answer is cached. 0 disables the cache.</summary>
    public required int? SearchCacheSeconds { get; set; }
}

public class ProductsOptionsValidator : CustomValidator<ProductsOptions>
{
    public ProductsOptionsValidator()
    {
        RuleFor(x => x.ImportJobConcurrentMessageLimit)
            .GreaterThan(0)
            .WithMessage("ImportJobConcurrentMessageLimit must be greater than 0.");

        RuleFor(x => x.ImportBatchesPerMessage)
            .GreaterThan(0)
            .WithMessage("ImportBatchesPerMessage must be greater than 0.");

        RuleFor(x => x.ImportMaxStoredRowErrors)
            .GreaterThan(0)
            .WithMessage("ImportMaxStoredRowErrors must be greater than 0.");

        RuleFor(x => x.ExportJobConcurrentMessageLimit)
            .GreaterThan(0)
            .WithMessage("ExportJobConcurrentMessageLimit must be greater than 0.");

        RuleFor(x => x.ExportPageSize)
            .GreaterThan(0)
            .WithMessage("ExportPageSize must be greater than 0.");

        RuleFor(x => x.ExportMaxRows)
            .GreaterThan(0)
            .WithMessage("ExportMaxRows must be greater than 0.");

        RuleFor(x => x.JobHousekeepingCron)
            .NotEmpty()
            .WithMessage("JobHousekeepingCron must not be empty.");

        RuleFor(x => x.ImageMaxFileBytes)
            .GreaterThan(0)
            .WithMessage("ImageMaxFileBytes must be greater than 0.");

        RuleFor(x => x.MaxImagesPerProduct)
            .GreaterThan(0)
            .WithMessage("MaxImagesPerProduct must be greater than 0.");

        RuleFor(x => x.MaxAttributesPerProduct)
            .GreaterThan(0)
            .WithMessage("MaxAttributesPerProduct must be greater than 0.");

        RuleFor(x => x.ProductsPageMaxSize)
            .GreaterThan(0)
            .WithMessage("ProductsPageMaxSize must be greater than 0.");

        RuleFor(x => x.SearchCacheSeconds)
            .NotNull()
            .GreaterThanOrEqualTo(0)
            .WithMessage("SearchCacheSeconds must be 0 or greater.");
    }
}
