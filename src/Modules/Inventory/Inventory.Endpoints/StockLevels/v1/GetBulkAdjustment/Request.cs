using Common.Application.Localization.Resources;
using Common.Application.ModelBinders;
using Common.Application.Validation;
using FluentValidation;
using Inventory.Application.BulkAdjustments;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Endpoints.StockLevels.v1.GetBulkAdjustment;

public sealed record Request
{
    [FromRoute]
    [ModelBinder<StronglyTypedIdBinder<StockBulkAdjustJobId>>]
    public StockBulkAdjustJobId Id { get; init; }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(localizer.StockLevels_GetBulkAdjustment_Id_NotEmpty);
    }
}
