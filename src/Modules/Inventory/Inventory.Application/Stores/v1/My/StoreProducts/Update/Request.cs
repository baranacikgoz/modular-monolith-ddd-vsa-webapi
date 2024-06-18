using Common.Application.Validation;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Inventory.Application.Stores.v1.My.StoreProducts.Update;

internal sealed record Request(int? Quantity, decimal? Price);

internal class RequestValidator : CustomValidator<Request>
{
    private const int QuantityGreaterThanOrEqualTo = 0;
    private const decimal PriceGreaterThan = 0;
    public RequestValidator(IStringLocalizer<RequestValidator> localizer)
    {
        RuleFor(r => r)
           .Must(r => r.Quantity.HasValue || r.Price.HasValue)
               .WithMessage(localizer["Stores.v1.My.StoreProducts.Update.AtLeastOnePropertyIsRequired"]);

        RuleFor(r => r.Quantity)
            .GreaterThanOrEqualTo(QuantityGreaterThanOrEqualTo)
                .WithMessage(localizer["Stores.v1.My.StoreProducts.Update.Quantity.GreaterThanOrEqualTo {0}", QuantityGreaterThanOrEqualTo])
            .When(r => r.Quantity.HasValue);

        RuleFor(r => r.Price)
            .GreaterThan(PriceGreaterThan)
                .WithMessage(localizer["Stores.v1.My.StoreProduct.Update.Price.GreaterThan {0}", PriceGreaterThan])
            .When(r => r.Price.HasValue);
    }
}
