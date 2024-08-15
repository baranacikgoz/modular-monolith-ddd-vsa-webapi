using Common.Application.Validation;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Inventory.Application.StoreProducts.v1.My.Update;

internal sealed record Request(int? Quantity, decimal? Price);

internal class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IStringLocalizer<RequestValidator> localizer)
    {
        RuleFor(r => r)
           .Must(r => r.Quantity.HasValue || r.Price.HasValue)
               .WithMessage(localizer["Stores.v1.My.StoreProducts.Update.AtLeastOnePropertyIsRequired"]);

        RuleFor(r => r.Quantity)
            .GreaterThanOrEqualTo(Domain.StoreProducts.Constants.QuantityGreaterThanOrEqualTo)
                .WithMessage(localizer["Stores.v1.My.StoreProducts.Update.Quantity.GreaterThanOrEqualTo {0}", Domain.StoreProducts.Constants.QuantityGreaterThanOrEqualTo])
            .When(r => r.Quantity.HasValue);

        RuleFor(r => r.Price)
            .GreaterThan(Domain.StoreProducts.Constants.PriceGreaterThan)
                .WithMessage(localizer["Stores.v1.My.StoreProduct.Update.Price.GreaterThan {0}", Domain.StoreProducts.Constants.PriceGreaterThan])
            .When(r => r.Price.HasValue);
    }
}
