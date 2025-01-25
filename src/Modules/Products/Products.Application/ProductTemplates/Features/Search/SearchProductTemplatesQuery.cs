using Common.Application.CQS;
using Common.Application.Localization;
using Common.Application.Queries.Pagination;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Products.Application.ProductTemplates.DTOs;

namespace Products.Application.ProductTemplates.Features.Search;

public sealed record SearchProductTemplatesQuery : PaginationQuery, IQuery<PaginationResult<ProductTemplateDto>>
{
    public string? Brand { get; init; }
    public string? Model { get; init; }
    public string? Color { get; init; }
}

public sealed class SearchProductsQueryValidator : PaginationQueryValidator<SearchProductTemplatesQuery>
{
    public SearchProductsQueryValidator(IStringLocalizer<ResxLocalizer> localizer) : base(localizer)
    {
        RuleFor(x => x.Brand)
            .MaximumLength(Domain.ProductTemplates.Constants.BrandMaxLength)
                .WithMessage(localizer["ProductTemplates.Search.Brand.MaximumLength {0}", Domain.ProductTemplates.Constants.BrandMaxLength])
            .When(x => x.Brand is not null);

        RuleFor(x => x.Model)
            .MaximumLength(Domain.ProductTemplates.Constants.ModelMaxLength)
                .WithMessage(localizer["ProductTemplates.Search.Model.MaximumLength {0}", Domain.ProductTemplates.Constants.ModelMaxLength])
            .When(x => x.Model is not null);

        RuleFor(x => x.Color)
            .MaximumLength(Domain.ProductTemplates.Constants.ColorMaxLength)
                .WithMessage(localizer["ProductTemplates.Search.Color.MaximumLength {0}", Domain.ProductTemplates.Constants.ColorMaxLength])
            .When(x => x.Color is not null);
    }
}
