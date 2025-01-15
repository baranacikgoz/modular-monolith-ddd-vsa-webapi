using Common.Application.Localization;
using Common.Application.Pagination;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Products.Application.StoreProducts.v1.Search;

public sealed class Request : PaginationRequest
{
    [FromQuery(Name = "name")]
    public string? Name { get; init; }

    [FromQuery(Name = "description")]
    public string? Description { get; init; }

    [FromQuery(Name = "minQuantity")]
    public int? MinQuantity { get; init; }

    [FromQuery(Name = "maxQuantity")]
    public int? MaxQuantity { get; init; }

    [FromQuery(Name = "minPrice")]
    public decimal? MinPrice { get; init; }

    [FromQuery(Name = "maxPrice")]
    public decimal? MaxPrice { get; init; }

    public Request() { }
}

public sealed class RequestValidator : PaginationRequestValidator<Request>
{
    public RequestValidator(IStringLocalizer<ResxLocalizer> localizer) : base(localizer)
    {
        RuleFor(x => x.Name)
            .MaximumLength(Domain.Stores.Constants.NameMaxLength)
                .WithMessage(localizer["Stores.v1.Search.Name.MaximumLength {0}", Domain.Stores.Constants.NameMaxLength])
            .When(x => x.Name is not null);

        RuleFor(x => x.Description)
            .MaximumLength(Domain.Stores.Constants.DescriptionMaxLength)
                .WithMessage(localizer["Stores.v1.Search.Description.MaximumLength {0}", Domain.Stores.Constants.DescriptionMaxLength])
            .When(x => x.Description is not null);

        RuleFor(x => x.MinQuantity)
            .GreaterThanOrEqualTo(Domain.StoreProducts.Constants.QuantityGreaterThanOrEqualTo)
                .WithMessage(localizer["Stores.v1.Search.MinQuantity.GreaterThanOrEqualTo {0}", Domain.StoreProducts.Constants.QuantityGreaterThanOrEqualTo])
            .When(x => x.MinQuantity is not null);

        RuleFor(x => x.MaxQuantity)
            .GreaterThanOrEqualTo(Domain.StoreProducts.Constants.QuantityGreaterThanOrEqualTo)
                .WithMessage(localizer["Stores.v1.Search.MaxQuantity.GreaterThanOrEqualTo {0}", Domain.StoreProducts.Constants.QuantityGreaterThanOrEqualTo])
            .When(x => x.MaxQuantity is not null);

        RuleFor(x => x.MinPrice)
            .GreaterThan(Domain.StoreProducts.Constants.PriceGreaterThan)
                .WithMessage(localizer["Stores.v1.Search.MinPrice.GreaterThan {0}", Domain.StoreProducts.Constants.PriceGreaterThan])
            .When(x => x.MinPrice is not null);

        RuleFor(x => x.MaxPrice)
            .GreaterThan(Domain.StoreProducts.Constants.PriceGreaterThan)
                .WithMessage(localizer["Stores.v1.Search.MaxPrice.GreaterThan {0}", Domain.StoreProducts.Constants.PriceGreaterThan])
            .When(x => x.MaxPrice is not null);
    }
}
