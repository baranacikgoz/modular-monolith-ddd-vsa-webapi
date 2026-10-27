using Common.Application.Localization.Resources;
using Common.Application.ModelBinders;
using Common.Application.Pagination;
using FluentValidation;
using Inventory.Domain.StockLevels;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Endpoints.StockLevels.v1.Movements;

public sealed record Request : PaginationRequest
{
    [FromRoute]
    [ModelBinder<StronglyTypedIdBinder<StockLevelId>>]
    public required StockLevelId Id { get; init; }
}

public sealed class RequestValidator : PaginationRequestValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer) : base(localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(localizer.StockLevels_Movements_Id_NotEmpty);
    }
}
