using System.Globalization;
using Common.Application.Localization.Resources;
using Common.Application.Pagination;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Constants = Products.Domain.ProductTemplates.Constants;

namespace Products.Endpoints.ProductTemplates.v1.Search;

public sealed record Request : PaginationRequest
{
    [FromQuery] public string? SearchTerm { get; init; }

    [FromQuery] public string? Brand { get; init; }

    [FromQuery] public string? Model { get; init; }

    [FromQuery] public string? Color { get; init; }
}

public sealed class RequestValidator : PaginationRequestValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer) : base(localizer)
    {
        RuleFor(x => x.SearchTerm)
            .MaximumLength(Constants.SearchTermMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.ProductTemplates_Search_SearchTerm_MaximumLength,
                Constants.SearchTermMaxLength))
            .When(x => x.SearchTerm is not null);

        RuleFor(x => x.Brand)
            .MaximumLength(Constants.BrandMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.ProductTemplates_Search_Brand_MaximumLength,
                Constants.BrandMaxLength))
            .When(x => x.Brand is not null);

        RuleFor(x => x.Model)
            .MaximumLength(Constants.ModelMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.ProductTemplates_Search_Model_MaximumLength,
                Constants.ModelMaxLength))
            .When(x => x.Model is not null);

        RuleFor(x => x.Color)
            .MaximumLength(Constants.ColorMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.ProductTemplates_Search_Color_MaximumLength,
                Constants.ColorMaxLength))
            .When(x => x.Color is not null);
    }
}
