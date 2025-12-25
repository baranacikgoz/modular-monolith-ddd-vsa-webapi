using Common.Application.Localization;
using Common.Application.ModelBinders;
using Common.Application.Pagination;
using Common.Domain.StronglyTypedIds;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Products.Domain.Stores;
using Constants = Products.Domain.Products.Constants;

namespace Products.Endpoints.Products.v1.Search;

public sealed record Request : PaginationRequest
{
    [FromQuery]
    [ModelBinder<StronglyTypedIdBinder<StoreId>>]
    public StoreId? StoreId { get; set; }

    [FromQuery]
    [ModelBinder<StronglyTypedIdBinder<ApplicationUserId>>]
    public ApplicationUserId? OwnerId { get; set; }

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
        RuleFor(x => x.StoreId)
            .NotEmpty()
            .WithMessage(localizer["Products.Search.StoreId.NotEmpty"])
            .When(x => x.StoreId is not null);

        RuleFor(x => x.OwnerId)
            .NotEmpty()
            .WithMessage(localizer["Products.Search.OwnerId.NotEmpty"])
            .When(x => x.OwnerId is not null);

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
