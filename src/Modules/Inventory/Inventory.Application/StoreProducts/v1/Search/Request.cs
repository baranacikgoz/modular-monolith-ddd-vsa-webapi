using Common.Application.Localization;
using Common.Application.Pagination;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Inventory.Application.StoreProducts.v1.Search;

public sealed record Request(
    string? Name,
    string? Description,
    int? MinQuantity,
    int? MaxQuantity,
    decimal? MinPrice,
    decimal? MaxPrice,
    int PageNumber,
    int PageSize)
    : PaginationRequest(PageNumber, PageSize);

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
