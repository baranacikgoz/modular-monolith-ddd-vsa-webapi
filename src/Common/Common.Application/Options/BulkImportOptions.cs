using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

/// <summary>Shared by the spreadsheet upload endpoints (product import, stock adjustment). The endpoint only stores
/// the upload and queues a job; what keeps the job safe is a hard ceiling on the input and chunked saves.</summary>
public class BulkImportOptions
{
    /// <summary>Uploads larger than this are rejected before the workbook is opened.</summary>
    public required long MaxFileBytes { get; set; }

    /// <summary>Data rows (header excluded, all worksheets together) beyond this reject the whole upload: the rows
    /// are held in memory while parsed, so this is the memory ceiling of one import.</summary>
    public required int MaxRows { get; set; }

    /// <summary>How many rows (or row groups) are upserted per SaveChanges; the change tracker is cleared after each
    /// chunk.</summary>
    public required int SaveChunkSize { get; set; }
}

public class BulkImportOptionsValidator : CustomValidator<BulkImportOptions>
{
    public BulkImportOptionsValidator()
    {
        RuleFor(x => x.MaxFileBytes)
            .GreaterThan(0)
            .WithMessage("MaxFileBytes must be greater than 0.");

        RuleFor(x => x.MaxRows)
            .GreaterThan(0)
            .WithMessage("MaxRows must be greater than 0.");

        RuleFor(x => x.SaveChunkSize)
            .GreaterThan(0)
            .WithMessage("SaveChunkSize must be greater than 0.");
    }
}
