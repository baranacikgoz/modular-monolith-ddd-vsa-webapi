using Common.Application.Validation;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Inventory.Application.Stores.v1.My.Products.Update;

internal sealed record Request(int? Quantity, decimal? Price);

internal class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IStringLocalizer<RequestValidator> localizer)
    {
        RuleFor(r => r)
           .Must(r => r.Quantity.HasValue || r.Price.HasValue)
               .WithMessage(localizer["AtLeastOnePropertyIsRequired"]);

        RuleFor(r => r.Quantity)
            .GreaterThanOrEqualTo(0)
                .WithMessage(localizer["Quantity.GreaterThanOrEqualTo", 0])
            .When(r => r.Quantity.HasValue);

        RuleFor(r => r.Price)
            .GreaterThanOrEqualTo(0)
                .WithMessage(localizer["Price.GreaterThanOrEqualTo", 0])
            .When(r => r.Price.HasValue);
    }
}
