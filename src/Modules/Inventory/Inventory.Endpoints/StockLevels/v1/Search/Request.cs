using System.Globalization;
using Common.Application.Localization.Resources;
using Common.Application.Pagination;
using FluentValidation;
using Inventory.Domain.StockLevels;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Endpoints.StockLevels.v1.Search;

public sealed record Request : PaginationRequest
{
    /// <summary>Free-text product name search, answered by Products. A blank value means no filter.</summary>
    [FromQuery] public string? ProductName { get; init; }
}

public sealed class RequestValidator : PaginationRequestValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer) : base(localizer)
    {
        RuleFor(x => x.ProductName)
            .MaximumLength(Constants.ProductNameSearchMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture,
                localizer.StockLevels_Search_ProductName_MaximumLength, Constants.ProductNameSearchMaxLength))
            .When(x => x.ProductName is not null);
    }
}
