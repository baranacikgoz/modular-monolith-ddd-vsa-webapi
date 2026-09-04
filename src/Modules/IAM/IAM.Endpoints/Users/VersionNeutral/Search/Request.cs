using System.Globalization;
using Common.Application.Localization.Resources;
using Common.Application.Pagination;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Constants = IAM.Domain.Users.Constants;

namespace IAM.Endpoints.Users.VersionNeutral.Search;

public sealed record Request : PaginationRequest
{
    /// <summary>Substring matched by Keycloak against username, first name, last name and email.</summary>
    [FromQuery] public string? SearchTerm { get; init; }
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
    }
}
