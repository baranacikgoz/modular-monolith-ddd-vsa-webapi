using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Application.Localization;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Common.Application.Pagination;
public class PaginationRequestValidator<T> : CustomValidator<T>
    where T : PaginationRequest
{
    private const int MaxPageSize = 1000;
    public PaginationRequestValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1)
                .WithMessage(localizer["PaginationRequest.PageNumber.GreaterThanOrEqualToOne"]);

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1)
                .WithMessage(localizer["PaginationRequest.PageSize.GreaterThanOrEqualToOne"])
            .LessThanOrEqualTo(MaxPageSize)
                .WithMessage(localizer["PaginationRequest.PageSize.LessThanOrEqualToMaxPageSize", MaxPageSize]);
    }
}
