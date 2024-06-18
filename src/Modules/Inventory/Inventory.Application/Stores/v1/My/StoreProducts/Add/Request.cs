using System.Text.Json.Serialization;
using Common.Application.JsonConverters;
using Common.Application.Localization;
using Common.Application.Validation;
using FluentValidation;
using Inventory.Domain.Products;
using Microsoft.Extensions.Localization;

namespace Inventory.Application.Stores.v1.My.StoreProducts.Add;

public sealed record Request(
    int Quantity,
    decimal Price)
{
    [JsonConverter(typeof(StronglyTypedIdReadOnlyJsonConverter<ProductId>))]
    public ProductId ProductId { get; init; }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    private const int QuantityGreaterThanOrEqualTo = 0;
    private const decimal PriceGreaterThan = 0;
    public RequestValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
                .WithMessage(localizer["Stores.v1.My.StoreProducts.Add.ProductId.NotEmpty"]);

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(QuantityGreaterThanOrEqualTo)
                .WithMessage(localizer["Stores.v1.My.StoreProducts.Add.Quantity.GreaterThanOrEqualTo {0}", QuantityGreaterThanOrEqualTo]);

        RuleFor(x => x.Price)
            .GreaterThan(PriceGreaterThan)
                .WithMessage(localizer["Stores.v1.My.StoreProducts.Add.Price.GreaterThan {0}", PriceGreaterThan]);
    }
}
