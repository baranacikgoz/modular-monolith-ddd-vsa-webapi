using System.Text.Json.Serialization;
using Common.Application.JsonConverters;
using Common.Application.Localization;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Products.Domain.Products;
using Products.Domain.Stores;

namespace Products.Application.StoreProducts.v1.AddToStore;

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
    public RequestValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.StoreId)
            .NotEmpty()
                .WithMessage(localizer["StoreProducts.v1.AddToStore.StoreId.NotEmpty"]);

        RuleFor(x => x.ProductId)
            .NotEmpty()
                .WithMessage(localizer["StoreProducts.v1.AddToStore.ProductId.NotEmpty"]);

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(Domain.StoreProducts.Constants.QuantityGreaterThanOrEqualTo)
                .WithMessage(localizer["StoreProducts.v1.AddToStore.Quantity.GreaterThanOrEqualTo {0}", Domain.StoreProducts.Constants.QuantityGreaterThanOrEqualTo]);

        RuleFor(x => x.Price)
            .GreaterThan(Domain.StoreProducts.Constants.PriceGreaterThan)
                .WithMessage(localizer["StoreProducts.v1.AddToStore.Price.GreaterThan {0}", Domain.StoreProducts.Constants.PriceGreaterThan]);
    }
}
