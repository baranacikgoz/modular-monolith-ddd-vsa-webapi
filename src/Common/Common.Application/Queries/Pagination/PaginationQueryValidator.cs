using Common.Application.Localization;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Common.Application.Queries.Pagination;

public class PaginationQueryValidator<T> : CustomValidator<T>
    where T : PaginationQuery
{
    private const int PageNumberGreaterThanOrEqualTo = 1;
    private const int PageSizeInclusiveMin = 1;
    private const int PageSizeInclusiveMax = 1000;
    public PaginationQueryValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(PageNumberGreaterThanOrEqualTo)
                .WithMessage(localizer["PaginationRequest.PageNumber.GreaterThanOrEqualTo {0}", PageNumberGreaterThanOrEqualTo]);

        RuleFor(x => x.PageSize)
            .InclusiveBetween(PageSizeInclusiveMin, PageSizeInclusiveMax)
                .WithMessage(localizer["PaginationRequest.PageSize.InclusiveBetween {0} {1}", PageSizeInclusiveMin, PageSizeInclusiveMax]);
    }
}
