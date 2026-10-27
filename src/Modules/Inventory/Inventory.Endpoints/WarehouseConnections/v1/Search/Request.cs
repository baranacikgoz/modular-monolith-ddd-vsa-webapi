using Common.Application.Localization.Resources;
using Common.Application.Pagination;
using FluentValidation;
using Inventory.Domain.WarehouseConnections;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Endpoints.WarehouseConnections.v1.Search;

public sealed record Request : PaginationRequest
{
    [FromQuery] public DefaultIdType? StoreId { get; init; }

    [FromQuery] public WarehouseConnectionStatus? Status { get; init; }
}

public sealed class RequestValidator : PaginationRequestValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer) : base(localizer)
    {
        RuleFor(x => x.StoreId)
            .NotEmpty()
            .WithMessage(localizer.WarehouseConnections_Search_StoreId_NotEmpty)
            .When(x => x.StoreId is not null);

        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage(localizer.WarehouseConnections_Search_Status_Invalid)
            .When(x => x.Status is not null);
    }
}
