using System.Text.Json.Serialization;
using Common.Application.JsonConverters;
using Common.Application.Localization;
using Common.Application.Validation;
using FluentValidation;
using Inventory.Domain.Products;
using Inventory.Domain.Stores;
using Microsoft.Extensions.Localization;

namespace Inventory.Application.StoreProducts.v1.AddToStore;

public sealed record Request(
    int Quantity,
    decimal Price)
{
    [JsonConverter(typeof(StronglyTypedIdReadOnlyJsonConverter<StoreId>))]
    public StoreId StoreId { get; init; }

    [JsonConverter(typeof(StronglyTypedIdReadOnlyJsonConverter<ProductId>))]
    public ProductId ProductId { get; init; }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    private const int QuantityGreaterThanOrEqualTo = 0;
    private const int PriceGreaterThan = 0;
    public RequestValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.StoreId)
            .NotEmpty()
                .WithMessage(localizer["StoreProducts.v1.AddToStore.StoreId.NotEmpty"]);

        RuleFor(x => x.ProductId)
            .NotEmpty()
                .WithMessage(localizer["StoreProducts.v1.AddToStore.ProductId.NotEmpty"]);

        // A store owner may want to list a product but it may be out-of-stock for now. So 0 is allowed.
        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(QuantityGreaterThanOrEqualTo)
                .WithMessage(localizer["StoreProducts.v1.AddToStore.Quantity.GreaterThanOrEqualTo {0}", QuantityGreaterThanOrEqualTo]);

        RuleFor(x => x.Price)
            .GreaterThan(PriceGreaterThan)
                .WithMessage(localizer["StoreProducts.v1.AddToStore.Price.GreaterThan {0}", PriceGreaterThan]);
    }
}
