using System.Globalization;
using Common.Application.Localization.Resources;
using Common.Application.Validation;
using FluentValidation;
using Inventory.Application.BulkAdjustments;
using Inventory.Domain.StockLevels;
using Microsoft.AspNetCore.Http;

namespace Inventory.Endpoints.StockLevels.v1.BulkAdjust;

public sealed record Request
{
    /// <summary>An .xlsx or .xls workbook; the first worksheet is read, row 1 is the header. Columns: A product id, B signed
    /// quantity (plus adds stock, minus removes it).</summary>
    public required IFormFile File { get; init; }

    /// <summary>Chosen by the client for this upload. Sending the same id again returns the first job instead of queueing
    /// a second adjustment.</summary>
    public required string ClientRequestId { get; init; }

    /// <summary>The explanation every ledger row of this adjustment carries.</summary>
    public required string Reason { get; init; }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    private static readonly string[] AllowedExtensions = [".xlsx", ".xls"];

    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.File)
            .NotNull()
            .WithMessage(localizer.StockLevels_BulkAdjust_File_NotEmpty)
            .Must(file => file.Length > 0)
            .WithMessage(localizer.StockLevels_BulkAdjust_File_NotEmpty)
            .Must(file => AllowedExtensions.Contains(Path.GetExtension(file.FileName), StringComparer.OrdinalIgnoreCase))
            .WithMessage(localizer.StockLevels_BulkAdjust_File_InvalidExtension);

        RuleFor(x => x.ClientRequestId)
            .NotEmpty()
            .WithMessage(localizer.StockLevels_BulkAdjust_ClientRequestId_NotEmpty)
            .MaximumLength(StockBulkAdjustJob.ClientRequestIdMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.StockLevels_BulkAdjust_ClientRequestId_MaxLength,
                StockBulkAdjustJob.ClientRequestIdMaxLength));

        RuleFor(x => x.Reason)
            .NotEmpty()
            .WithMessage(localizer.StockLevels_BulkAdjust_Reason_NotEmpty)
            .MaximumLength(Constants.ReasonMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.StockLevels_BulkAdjust_Reason_MaximumLength, Constants.ReasonMaxLength));
    }
}
