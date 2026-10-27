using System.Globalization;
using Common.Application.Localization.Resources;
using Common.Application.ModelBinders;
using Common.Application.Validation;
using FluentValidation;
using Inventory.Domain.StockReservations;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Endpoints.StockReservations.v1.ResolveReadOnly;

public sealed record Request
{
    [FromRoute]
    [ModelBinder<StronglyTypedIdBinder<StockReservationId>>]
    public required StockReservationId Id { get; init; }

    [FromBody] public required RequestBody Body { get; init; }

    public sealed class RequestBody
    {
        /// <summary>What the reservation really is at the warehouse system: Released, Committed or Expired.</summary>
        public required ReservationStatus TargetStatus { get; init; }

        /// <summary>The warehouse system's reference. Needed only when the target is Committed and the reservation is not committed yet.</summary>
        public string? ProviderReference { get; init; }
    }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(localizer.StockReservations_ResolveReadOnly_Id_NotEmpty);

        RuleFor(x => x.Body)
            .NotEmpty()
            .SetValidator(new RequestBodyValidator(localizer));
    }
}

public sealed class RequestBodyValidator : CustomValidator<Request.RequestBody>
{
    public RequestBodyValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.TargetStatus)
            .Must(status => status is ReservationStatus.Released or ReservationStatus.Committed or ReservationStatus.Expired)
            .WithMessage(localizer.StockReservations_ResolveReadOnly_TargetStatus_Invalid);

        RuleFor(x => x.ProviderReference)
            .MaximumLength(Constants.ProviderReferenceMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture,
                localizer.StockReservations_ResolveReadOnly_ProviderReference_MaximumLength, Constants.ProviderReferenceMaxLength));
    }
}
