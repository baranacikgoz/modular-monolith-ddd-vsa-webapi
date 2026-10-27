using Common.Application.Localization.Resources;
using Common.Application.ModelBinders;
using Common.Application.Validation;
using FluentValidation;
using Inventory.Domain.WarehouseConnections;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Endpoints.WarehouseConnections.v1.Deactivate;

public sealed record Request
{
    [FromRoute]
    [ModelBinder<StronglyTypedIdBinder<WarehouseConnectionId>>]
    public required WarehouseConnectionId Id { get; init; }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(localizer.WarehouseConnections_Deactivate_Id_NotEmpty);
    }
}
