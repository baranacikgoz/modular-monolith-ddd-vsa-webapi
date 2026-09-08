using System.Text.Json.Serialization;
using Common.Application.JsonConverters;
using Common.Application.Localization.Resources;
using Common.Application.Validation;
using FluentValidation;
using Inventory.Domain.StockReservations;

namespace Inventory.Endpoints.StockReservations.v1.WebhookCallback;

/// <summary>
///     Bound manually from the raw request body (see <see cref="Endpoint"/>) rather than via
///     <c>[FromBody]</c> auto-binding, because the HMAC signature must be verified against the exact raw
///     bytes BEFORE any deserialization happens.
/// </summary>
public sealed record Request
{
    [JsonConverter(typeof(StronglyTypedIdReadOnlyJsonConverter<StockReservationId>))]
    public required StockReservationId ReservationId { get; init; }

    public required string ProviderReference { get; init; }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.ReservationId)
            .NotEmpty()
            .WithMessage(localizer.StockReservations_WebhookCallback_ReservationId_NotEmpty);

        RuleFor(x => x.ProviderReference)
            .NotEmpty()
            .WithMessage(localizer.StockReservations_WebhookCallback_ProviderReference_NotEmpty);
    }
}
