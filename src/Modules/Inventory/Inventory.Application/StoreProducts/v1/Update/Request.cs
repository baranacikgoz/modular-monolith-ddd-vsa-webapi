using System.Text.Json.Serialization;
using Common.Application.JsonConverters;
using Common.Application.Validation;
using FluentValidation;
using Inventory.Domain.Products;
using Inventory.Domain.StoreProducts;
using Inventory.Domain.Stores;
using Microsoft.Extensions.Localization;

namespace Inventory.Application.StoreProducts.v1.Update;

public sealed record Request(int? Quantity, decimal? Price);

public class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IStringLocalizer<RequestValidator> localizer)
    {
        RuleFor(r => r)
           .Must(r => r.Quantity.HasValue || r.Price.HasValue)
               .WithMessage(localizer["StoreProducts.v1.Update.AtLeastOnePropertyIsRequired"]);

        RuleFor(r => r.Quantity)
            .GreaterThanOrEqualTo(Domain.StoreProducts.Constants.QuantityGreaterThanOrEqualTo)
                .WithMessage(localizer["StoreProducts.v1.Update.Quantity.GreaterThanOrEqualTo {0}", Domain.StoreProducts.Constants.QuantityGreaterThanOrEqualTo])
            .When(r => r.Quantity.HasValue);

        RuleFor(r => r.Price)
            .GreaterThan(Domain.StoreProducts.Constants.PriceGreaterThan)
                .WithMessage(localizer["StoreProducts.v1.Update.Price.GreaterThan {0}", Domain.StoreProducts.Constants.PriceGreaterThan])
            .When(r => r.Price.HasValue);
    }
}
