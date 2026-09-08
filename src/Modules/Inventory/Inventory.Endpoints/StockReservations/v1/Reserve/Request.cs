using Common.Application.Localization.Resources;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Endpoints.StockReservations.v1.Reserve;

public sealed record Request
{
    [FromBody] public required RequestBody Body { get; init; }

    public sealed class RequestBody
    {
        public required DefaultIdType ProductId { get; init; }
        public required int Quantity { get; init; }
        public required DateTimeOffset ReservationDeadline { get; init; }
    }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.Body)
            .NotEmpty()
            .SetValidator(new RequestBodyValidator(localizer));
    }
}

public sealed class RequestBodyValidator : CustomValidator<Request.RequestBody>
{
    public RequestBodyValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage(localizer.StockReservations_Reserve_ProductId_NotEmpty);

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage(localizer.StockReservations_Reserve_Quantity_GreaterThanZero);

        RuleFor(x => x.ReservationDeadline)
            .NotEmpty()
            .WithMessage(localizer.StockReservations_Reserve_ReservationDeadline_NotEmpty);
    }
}
