using Common.Application.Localization.Resources;
using Common.Application.Pagination;
using FluentValidation;
using Inventory.Domain.StockReservations;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Endpoints.StockReservations.v1.My.Search;

public sealed record Request : PaginationRequest
{
    [FromQuery] public DefaultIdType? ProductId { get; init; }

    [FromQuery] public ReservationStatus? Status { get; init; }
}

public sealed class RequestValidator : PaginationRequestValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer) : base(localizer)
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage(localizer.StockReservations_MySearch_ProductId_NotEmpty)
            .When(x => x.ProductId is not null);

        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage(localizer.StockReservations_MySearch_Status_Invalid)
            .When(x => x.Status is not null);
    }
}
