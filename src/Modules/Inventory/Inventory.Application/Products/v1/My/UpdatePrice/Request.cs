using Common.Application.Validation;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Inventory.Application.Products.v1.My.UpdatePrice;

internal sealed record Request(decimal Price);

internal class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IStringLocalizer<RequestValidator> localizer)
    {
        RuleFor(r => r.Price)
            .GreaterThanOrEqualTo(0)
                .WithMessage(localizer["Price.GreaterThanOrEqualTo", 0]);
    }
}
