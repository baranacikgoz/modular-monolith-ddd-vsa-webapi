using System.Globalization;
using Common.Application.Localization.Resources;
using Common.Application.Pagination;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Constants = IAM.Domain.Identity.Constants;

namespace IAM.Endpoints.Users.VersionNeutral.Search;

public sealed record Request : PaginationRequest
{
    [FromQuery] public string? SearchTerm { get; init; }

    [FromQuery] public string? FullName { get; init; }
}

public sealed class RequestValidator : PaginationRequestValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer) : base(localizer)
    {
        RuleFor(x => x.SearchTerm)
            .MaximumLength(Constants.SearchTermMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Users_Search_SearchTerm_MaximumLength,
                Constants.SearchTermMaxLength))
            .When(x => x.SearchTerm is not null);

        RuleFor(x => x.FullName)
            .MaximumLength(Constants.FullNameMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Users_Search_FullName_MaximumLength,
                Constants.FullNameMaxLength))
            .When(x => x.FullName is not null);
    }
}
