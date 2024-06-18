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
                .WithMessage(localizer["Stores.v1.Search.Name.MaximumLength", Domain.Stores.Constants.NameMaxLength])
            .When(x => x.Name is not null);

        RuleFor(x => x.Description)
            .MaximumLength(Domain.Stores.Constants.DescriptionMaxLength)
                .WithMessage(localizer["Stores.v1.Search.Description.MaximumLength", Domain.Stores.Constants.DescriptionMaxLength])
            .When(x => x.Description is not null);

        RuleFor(x => x.MinQuantity)
            .GreaterThanOrEqualTo(0)
                .WithMessage(localizer["Stores.v1.Search.MinQuantity.GreaterThanOrEqualToZero"])
            .When(x => x.MinQuantity is not null);

        RuleFor(x => x.MaxQuantity)
            .GreaterThanOrEqualTo(0)
                .WithMessage(localizer["Stores.v1.Search.MaxQuantity.GreaterThanOrEqualToZero"])
            .When(x => x.MaxQuantity is not null);

        RuleFor(x => x.MinPrice)
            .GreaterThanOrEqualTo(0)
                .WithMessage(localizer["Stores.v1.Search.MinPrice.GreaterThanOrEqualToZero"])
            .When(x => x.MinPrice is not null);

        RuleFor(x => x.MaxPrice)
            .GreaterThanOrEqualTo(0)
                .WithMessage(localizer["Stores.v1.Search.MaxPrice.GreaterThanOrEqualToZero"])
            .When(x => x.MaxPrice is not null);
    }
}
