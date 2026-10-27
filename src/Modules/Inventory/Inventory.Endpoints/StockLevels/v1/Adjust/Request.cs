using System.Globalization;
using Common.Application.Localization.Resources;
using Common.Application.ModelBinders;
using Common.Application.Validation;
using FluentValidation;
using Inventory.Domain.StockLevels;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Endpoints.StockLevels.v1.Adjust;

public sealed record Request
{
    [FromRoute]
    [ModelBinder<StronglyTypedIdBinder<StockLevelId>>]
    public required StockLevelId Id { get; init; }

    [FromBody] public required RequestBody Body { get; init; }

    public sealed class RequestBody
    {
        /// <summary>Signed: plus adds stock, minus removes it.</summary>
        public required int Quantity { get; init; }

        public required string Reason { get; init; }
    }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(localizer.StockLevels_Adjust_Id_NotEmpty);

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
            .NotEqual(0)
            .WithMessage(localizer.StockLevels_Adjust_Quantity_NotZero);

        RuleFor(x => x.Quantity)
            .InclusiveBetween(-Constants.QuantityMaxValue, Constants.QuantityMaxValue)
            .WithMessage(string.Format(CultureInfo.CurrentCulture,
                localizer.StockLevels_Adjust_Quantity_InclusiveBetween, -Constants.QuantityMaxValue, Constants.QuantityMaxValue));

        RuleFor(x => x.Reason)
            .NotEmpty()
            .WithMessage(localizer.StockLevels_Adjust_Reason_NotEmpty);

        RuleFor(x => x.Reason)
            .MaximumLength(Constants.ReasonMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture,
                localizer.StockLevels_Adjust_Reason_MaximumLength, Constants.ReasonMaxLength));
    }
}
