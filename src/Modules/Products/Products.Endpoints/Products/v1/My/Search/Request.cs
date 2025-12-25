using Common.Application.Localization;
using Common.Application.Pagination;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Products.Domain.Products;

namespace Products.Endpoints.Products.v1.My.Search;

public sealed record Request : PaginationRequest
{
    [FromQuery] public string? Name { get; init; }

    [FromQuery] public string? Description { get; init; }

    [FromQuery] public int? MinQuantity { get; init; }

    [FromQuery] public int? MaxQuantity { get; init; }

    [FromQuery] public decimal? MinPrice { get; init; }

    [FromQuery] public decimal? MaxPrice { get; init; }
}

public sealed class RequestValidator : PaginationRequestValidator<Request>
{
    public RequestValidator(IStringLocalizer<ResxLocalizer> localizer) : base(localizer)
    {
        RuleFor(x => x.Name)
            .MaximumLength(Constants.NameMaxLength)
            .WithMessage(localizer["Products.Search.Name.MaximumLength {0}", Constants.NameMaxLength])
            .When(x => x.Name is not null);

        RuleFor(x => x.Description)
            .MaximumLength(Constants.DescriptionMaxLength)
            .WithMessage(localizer["Products.Search.Description.MaximumLength {0}", Constants.DescriptionMaxLength])
            .When(x => x.Description is not null);

        RuleFor(x => x.MinQuantity)
            .GreaterThanOrEqualTo(Constants.QuantityGreaterThanOrEqualTo)
            .WithMessage(localizer["Products.Search.MinQuantity.GreaterThanOrEqualTo {0}",
                Constants.QuantityGreaterThanOrEqualTo])
            .When(x => x.MinQuantity is not null);

        RuleFor(x => x.MaxQuantity)
            .GreaterThanOrEqualTo(Constants.QuantityGreaterThanOrEqualTo)
            .WithMessage(localizer["Products.Search.MaxQuantity.GreaterThanOrEqualTo {0}",
                Constants.QuantityGreaterThanOrEqualTo])
            .When(x => x.MaxQuantity is not null);

        RuleFor(x => x.MinPrice)
            .GreaterThan(Constants.PriceGreaterThanOrEqualTo)
            .WithMessage(localizer["Products.Search.MinPrice.GreaterThanOrEqualTo {0}",
                Constants.PriceGreaterThanOrEqualTo])
            .When(x => x.MinPrice is not null);

        RuleFor(x => x.MaxPrice)
            .GreaterThan(Constants.PriceGreaterThanOrEqualTo)
            .WithMessage(localizer["Products.Search.MaxPrice.GreaterThanOrEqualTo {0}",
                Constants.PriceGreaterThanOrEqualTo])
            .When(x => x.MaxPrice is not null);
    }
}
