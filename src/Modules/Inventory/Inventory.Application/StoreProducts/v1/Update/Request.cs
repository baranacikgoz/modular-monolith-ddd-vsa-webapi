using System.Text.Json.Serialization;
using Common.Application.JsonConverters;
using Common.Application.Validation;
using FluentValidation;
using Inventory.Domain.Products;
using Inventory.Domain.StoreProducts;
using Inventory.Domain.Stores;
using Microsoft.Extensions.Localization;

namespace Inventory.Application.StoreProducts.v1.Update;

public sealed record Request(int? Quantity, decimal? Price)
{
    [JsonConverter(typeof(StronglyTypedIdReadOnlyJsonConverter<StoreId>))]
    public StoreId StoreId { get; init; }

    [JsonConverter(typeof(StronglyTypedIdReadOnlyJsonConverter<StoreProductId>))]
    public StoreProductId StoreProductId { get; init; }
}

public class RequestValidator : CustomValidator<Request>
{
    private const int QuantityGreaterThanOrEqualTo = 0;
    private const decimal PriceGreaterThan = 0;
    public RequestValidator(IStringLocalizer<RequestValidator> localizer)
    {
        RuleFor(x => x.StoreId)
            .NotEmpty()
                .WithMessage(localizer["StoreProducts.v1.Update.StoreId.NotEmpty"]);

        RuleFor(x => x.StoreProductId)
            .NotEmpty()
                .WithMessage(localizer["StoreProducts.v1.Update.StoreProductId.NotEmpty"]);

        RuleFor(r => r)
           .Must(r => r.Quantity.HasValue || r.Price.HasValue)
               .WithMessage(localizer["StoreProducts.v1.Update.AtLeastOnePropertyIsRequired"]);

        RuleFor(r => r.Quantity)
            .GreaterThanOrEqualTo(QuantityGreaterThanOrEqualTo)
                .WithMessage(localizer["StoreProducts.v1.Update.Quantity.GreaterThanOrEqualTo {0}", QuantityGreaterThanOrEqualTo])
            .When(r => r.Quantity.HasValue);

        RuleFor(r => r.Price)
            .GreaterThan(PriceGreaterThan)
                .WithMessage(localizer["StoreProducts.v1.Update.Price.GreaterThan {0}", PriceGreaterThan])
            .When(r => r.Price.HasValue);
    }
}
