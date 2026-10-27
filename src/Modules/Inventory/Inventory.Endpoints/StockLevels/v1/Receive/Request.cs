using System.Globalization;
using Common.Application.Localization.Resources;
using Common.Application.ModelBinders;
using Common.Application.Validation;
using FluentValidation;
using Inventory.Domain.StockLevels;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Endpoints.StockLevels.v1.Receive;

public sealed record Request
{
    [FromRoute]
    [ModelBinder<StronglyTypedIdBinder<StockLevelId>>]
    public required StockLevelId Id { get; init; }

    [FromBody] public required RequestBody Body { get; init; }

    public sealed class RequestBody
    {
        public required int Quantity { get; init; }
        public string? Reason { get; init; }
    }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(localizer.StockLevels_Receive_Id_NotEmpty);

        RuleFor(x => x.Body)
            .NotEmpty()
            .SetValidator(new RequestBodyValidator(localizer));
    }
}

public sealed class RequestBodyValidator : CustomValidator<Request.RequestBody>
{
    public RequestBodyValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.Quantity)
            .InclusiveBetween(1, Constants.QuantityMaxValue)
            .WithMessage(string.Format(CultureInfo.CurrentCulture,
                localizer.StockLevels_Receive_Quantity_InclusiveBetween, 1, Constants.QuantityMaxValue));

        RuleFor(x => x.Reason)
            .MaximumLength(Constants.ReasonMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture,
                localizer.StockLevels_Receive_Reason_MaximumLength, Constants.ReasonMaxLength))
            .When(x => x.Reason is not null);
    }
}
