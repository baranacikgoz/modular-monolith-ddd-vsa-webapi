using Common.Core.Contracts.Money;
using Common.Core.Validation;
using Microsoft.Extensions.Localization;

namespace Sales.Features.Products.UseCases.v1.My.UpdatePrice;

internal sealed record Request(Price Price);

internal class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IStringLocalizer<RequestValidator> localizer)
    {
        RuleFor(r => r.Price)
            .SetValidator(new PriceValidator(localizer));
    }
}
