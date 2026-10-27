using System.Globalization;
using Common.Application.Localization.Resources;
using Common.Application.ModelBinders;
using Common.Application.Validation;
using FluentValidation;
using Inventory.Domain.StockLevels;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Endpoints.StockLevels.v1.ChangeLowStockThreshold;

public sealed record Request
{
    [FromRoute]
    [ModelBinder<StronglyTypedIdBinder<StockLevelId>>]
    public required StockLevelId Id { get; init; }

    [FromBody] public required RequestBody Body { get; init; }

    public sealed class RequestBody
    {
        /// <summary>0 turns the low stock alert off.</summary>
        public required int Threshold { get; init; }
    }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(localizer.StockLevels_ChangeLowStockThreshold_Id_NotEmpty);

        RuleFor(x => x.Body)
            .NotEmpty()
            .SetValidator(new RequestBodyValidator(localizer));
    }
}

public sealed class RequestBodyValidator : CustomValidator<Request.RequestBody>
{
    public RequestBodyValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.Threshold)
            .InclusiveBetween(0, Constants.QuantityMaxValue)
            .WithMessage(string.Format(CultureInfo.CurrentCulture,
                localizer.StockLevels_ChangeLowStockThreshold_Threshold_InclusiveBetween, 0, Constants.QuantityMaxValue));
    }
}
