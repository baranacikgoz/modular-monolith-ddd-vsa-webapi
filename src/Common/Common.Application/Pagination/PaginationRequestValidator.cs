using Common.Application.Localization;
using Common.Application.Localization.Resources;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Common.Application.Pagination;

public class PaginationRequestValidator<T> : CustomValidator<T>
    where T : PaginationRequest
{
    private const int PageNumberGreaterThanOrEqualTo = 1;
    private const int PageSizeInclusiveMin = 1;
    private const int PageSizeInclusiveMax = 1000;

    public PaginationRequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(PageNumberGreaterThanOrEqualTo)
            .WithMessage(string.Format(System.Globalization.CultureInfo.CurrentCulture,
                localizer.PaginationRequest_PageNumber_GreaterThanOrEqualTo,
                PageNumberGreaterThanOrEqualTo));

        RuleFor(x => x.PageSize)
            .InclusiveBetween(PageSizeInclusiveMin, PageSizeInclusiveMax)
            .WithMessage(string.Format(System.Globalization.CultureInfo.CurrentCulture,
                localizer.PaginationRequest_PageSize_InclusiveBetween, PageSizeInclusiveMin,
                PageSizeInclusiveMax));
    }
}
