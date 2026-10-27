using System.Globalization;
using Common.Application.Localization.Resources;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Products.Application.Imports;

namespace Products.Endpoints.Products.v1.Import;

public sealed record Request
{
    /// <summary>An .xlsx or .xls workbook; the first worksheet is read, row 1 is the header. Columns: A name, B description
    /// (optional), C quantity, D price, E SKU (optional).</summary>
    public required IFormFile File { get; init; }

    /// <summary>Chosen by the client for this upload. Sending the same id again returns the first job instead of queueing
    /// a second import.</summary>
    public required string ClientRequestId { get; init; }

    /// <summary>The active product template every product of the file is created from.</summary>
    public required DefaultIdType ProductTemplateId { get; init; }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    private static readonly string[] AllowedExtensions = [".xlsx", ".xls"];

    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.File)
            .NotNull()
            .WithMessage(localizer.Products_Import_File_NotEmpty)
            .Must(file => file.Length > 0)
            .WithMessage(localizer.Products_Import_File_NotEmpty)
            .Must(file => AllowedExtensions.Contains(Path.GetExtension(file.FileName), StringComparer.OrdinalIgnoreCase))
            .WithMessage(localizer.Products_Import_File_InvalidExtension);

        RuleFor(x => x.ClientRequestId)
            .NotEmpty()
            .WithMessage(localizer.Products_Import_ClientRequestId_NotEmpty)
            .MaximumLength(ProductImportJob.ClientRequestIdMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Products_Import_ClientRequestId_MaxLength,
                ProductImportJob.ClientRequestIdMaxLength));

        RuleFor(x => x.ProductTemplateId)
            .NotEmpty()
            .WithMessage(localizer.Products_Import_ProductTemplateId_NotEmpty);
    }
}
