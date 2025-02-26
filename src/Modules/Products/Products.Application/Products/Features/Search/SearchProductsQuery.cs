using System.Linq.Expressions;
using Common.Application.CQS;
using Common.Application.Localization;
using Common.Application.Queries.Pagination;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Products.Domain.Products;
using Products.Domain.Stores;

namespace Products.Application.Products.Features.Search;

public sealed record SearchProductsQuery<TDto> : PaginationQuery<Product, TDto>, IQuery<PaginationResult<TDto>>
{
    public StoreId? StoreId { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public int? MinQuantity { get; init; }
    public int? MaxQuantity { get; init; }
    public decimal? MinPrice { get; init; }
    public decimal? MaxPrice { get; init; }
}

public sealed class SearchProductsQueryValidator<TDto> : PaginationQueryValidator<SearchProductsQuery<TDto>>
{
    public SearchProductsQueryValidator(IStringLocalizer<ResxLocalizer> localizer) : base(localizer)
    {
        RuleFor(x => x.StoreId)
            .NotEmpty()
                .WithMessage(localizer["Products.Search.StoreId.NotEmpty"])
            .When(x => x.StoreId is not null);

        RuleFor(x => x.Name)
            .MaximumLength(Domain.Products.Constants.NameMaxLength)
                .WithMessage(localizer["Products.Search.Name.MaximumLength {0}", Domain.Products.Constants.NameMaxLength])
            .When(x => x.Name is not null);

        RuleFor(x => x.Description)
            .MaximumLength(Domain.Products.Constants.DescriptionMaxLength)
                .WithMessage(localizer["Products.Search.Description.MaximumLength {0}", Domain.Products.Constants.DescriptionMaxLength])
            .When(x => x.Description is not null);

        RuleFor(x => x.MinQuantity)
            .GreaterThanOrEqualTo(Domain.Products.Constants.QuantityGreaterThanOrEqualTo)
                .WithMessage(localizer["Products.Search.MinQuantity.GreaterThanOrEqualTo {0}", Domain.Products.Constants.QuantityGreaterThanOrEqualTo])
            .When(x => x.MinQuantity is not null);

        RuleFor(x => x.MaxQuantity)
            .GreaterThanOrEqualTo(Domain.Products.Constants.QuantityGreaterThanOrEqualTo)
                .WithMessage(localizer["Products.Search.MaxQuantity.GreaterThanOrEqualTo {0}", Domain.Products.Constants.QuantityGreaterThanOrEqualTo])
            .When(x => x.MaxQuantity is not null);

        RuleFor(x => x.MinPrice)
            .GreaterThan(Domain.Products.Constants.PriceGreaterThanOrEqualTo)
                .WithMessage(localizer["Products.Search.MinPrice.GreaterThanOrEqualTo {0}", Domain.Products.Constants.PriceGreaterThanOrEqualTo])
            .When(x => x.MinPrice is not null);

        RuleFor(x => x.MaxPrice)
            .GreaterThan(Domain.Products.Constants.PriceGreaterThanOrEqualTo)
                .WithMessage(localizer["Products.Search.MaxPrice.GreaterThanOrEqualTo {0}", Domain.Products.Constants.PriceGreaterThanOrEqualTo])
            .When(x => x.MaxPrice is not null);
    }
}
