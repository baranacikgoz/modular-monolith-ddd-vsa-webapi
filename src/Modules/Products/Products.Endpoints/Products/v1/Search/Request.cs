using System.Globalization;
using Common.Application.Localization.Resources;
using Common.Application.ModelBinders;
using Common.Application.Pagination;
using Common.Domain.StronglyTypedIds;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
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

    [FromQuery] public string? SearchTerm { get; init; }

    [FromQuery] public string? Name { get; init; }

    [FromQuery] public string? Description { get; init; }

    [FromQuery] public int? MinQuantity { get; init; }

    [FromQuery] public int? MaxQuantity { get; init; }

    [FromQuery] public decimal? MinPrice { get; init; }

    [FromQuery] public decimal? MaxPrice { get; init; }
}

public sealed class RequestValidator : PaginationRequestValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer) : base(localizer)
    {
        RuleFor(x => x.SearchTerm)
            .MaximumLength(Constants.SearchTermMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Products_Search_SearchTerm_MaximumLength,
                Constants.SearchTermMaxLength))
            .When(x => x.SearchTerm is not null);

        RuleFor(x => x.StoreId)
            .NotEmpty()
            .WithMessage(localizer.Products_Search_StoreId_NotEmpty)
            .When(x => x.StoreId is not null);

        RuleFor(x => x.OwnerId)
            .NotEmpty()
            .WithMessage(localizer.Products_Search_OwnerId_NotEmpty)
            .When(x => x.OwnerId is not null);

        RuleFor(x => x.Name)
            .MaximumLength(Constants.NameMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Products_Search_Name_MaximumLength,
                Constants.NameMaxLength))
            .When(x => x.Name is not null);

        RuleFor(x => x.Description)
            .MaximumLength(Constants.DescriptionMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Products_Search_Description_MaximumLength,
                Constants.DescriptionMaxLength))
            .When(x => x.Description is not null);

        RuleFor(x => x.MinQuantity)
            .GreaterThanOrEqualTo(Constants.QuantityGreaterThanOrEqualTo)
            .WithMessage(string.Format(CultureInfo.CurrentCulture,
                localizer.Products_Search_MinQuantity_GreaterThanOrEqualTo,
                Constants.QuantityGreaterThanOrEqualTo))
            .When(x => x.MinQuantity is not null);

        RuleFor(x => x.MaxQuantity)
            .GreaterThanOrEqualTo(Constants.QuantityGreaterThanOrEqualTo)
            .WithMessage(string.Format(CultureInfo.CurrentCulture,
                localizer.Products_Search_MaxQuantity_GreaterThanOrEqualTo,
                Constants.QuantityGreaterThanOrEqualTo))
            .When(x => x.MaxQuantity is not null);

        RuleFor(x => x.MinPrice)
            .GreaterThan(Constants.PriceGreaterThanOrEqualTo)
            .WithMessage(string.Format(CultureInfo.CurrentCulture,
                localizer.Products_Search_MinPrice_GreaterThanOrEqualTo,
                Constants.PriceGreaterThanOrEqualTo))
            .When(x => x.MinPrice is not null);

        RuleFor(x => x.MaxPrice)
            .GreaterThan(Constants.PriceGreaterThanOrEqualTo)
            .WithMessage(string.Format(CultureInfo.CurrentCulture,
                localizer.Products_Search_MaxPrice_GreaterThanOrEqualTo,
                Constants.PriceGreaterThanOrEqualTo))
            .When(x => x.MaxPrice is not null);
    }
}
