using Common.Application.Localization.Resources;
using Common.Application.ModelBinders;
using Common.Application.Validation;
using FluentValidation;
using Inventory.Domain.StockReservations;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Endpoints.StockReservations.v1.Get;

public sealed record Request
{
    [FromRoute]
    [ModelBinder<StronglyTypedIdBinder<StockReservationId>>]
    public required StockReservationId Id { get; init; }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(localizer.StockReservations_Get_Id_NotEmpty);
    }
}
