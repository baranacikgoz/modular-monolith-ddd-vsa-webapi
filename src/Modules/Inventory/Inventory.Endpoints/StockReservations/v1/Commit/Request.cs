using Common.Application.Localization.Resources;
using Common.Application.ModelBinders;
using Common.Application.Validation;
using FluentValidation;
using Inventory.Domain.StockReservations;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Endpoints.StockReservations.v1.Commit;

public sealed record Request
{
    [FromRoute]
    [ModelBinder<StronglyTypedIdBinder<StockReservationId>>]
    public required StockReservationId Id { get; init; }

    [FromBody] public required RequestBody Body { get; init; }

    public sealed class RequestBody
    {
        public required string ProviderReference { get; init; }
    }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(localizer.StockReservations_Commit_Id_NotEmpty);

        RuleFor(x => x.Body)
            .NotEmpty()
            .SetValidator(new RequestBodyValidator(localizer));
    }
}

public sealed class RequestBodyValidator : CustomValidator<Request.RequestBody>
{
    public RequestBodyValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.ProviderReference)
            .NotEmpty()
            .WithMessage(localizer.StockReservations_Commit_ProviderReference_NotEmpty);
    }
}
