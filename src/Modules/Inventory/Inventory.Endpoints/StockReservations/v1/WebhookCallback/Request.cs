using System.Globalization;
using System.Text.Json.Serialization;
using Common.Application.JsonConverters;
using Common.Application.Localization.Resources;
using Common.Application.ModelBinders;
using Common.Application.Validation;
using FluentValidation;
using Inventory.Domain.StockReservations;
using Inventory.Domain.WarehouseConnections;
using Microsoft.AspNetCore.Mvc;
using DeliveryConstants = Inventory.Domain.WebhookDeliveries.Constants;
using ReservationConstants = Inventory.Domain.StockReservations.Constants;

namespace Inventory.Endpoints.StockReservations.v1.WebhookCallback;

/// <summary>The connection the delivery arrives on: bound by the framework, unlike <see cref="Request" />.</summary>
public sealed record RouteParameters
{
    [FromRoute]
    [ModelBinder<StronglyTypedIdBinder<WarehouseConnectionId>>]
    public required WarehouseConnectionId ConnectionId { get; init; }
}

public sealed class RouteParametersValidator : CustomValidator<RouteParameters>
{
    public RouteParametersValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.ConnectionId)
            .NotEmpty()
            .WithMessage(localizer.StockReservations_WebhookCallback_ConnectionId_NotEmpty);
    }
}

/// <summary>
///     Bound manually from the raw request body (see <see cref="Endpoint"/>) rather than via
///     <c>[FromBody]</c> auto-binding, because the HMAC signature must be verified against the exact raw
///     bytes BEFORE any deserialization happens.
/// </summary>
public sealed record Request
{
    /// <summary>The warehouse system's id of this delivery. A delivery id seen before on the connection is acknowledged and applies nothing.</summary>
    public required string DeliveryId { get; init; }

    [JsonConverter(typeof(StronglyTypedIdReadOnlyJsonConverter<StockReservationId>))]
    public required StockReservationId ReservationId { get; init; }

    /// <summary>The warehouse system's reference of the confirmation. Required when the delivery confirms the reservation, ignored otherwise.</summary>
    public string? ProviderReference { get; init; }

    /// <summary>
    ///     The warehouse system's own word for the status it reports. A body without one is the original confirmation delivery.
    ///     A word this system has no mapping for is acknowledged and recorded, never refused, so it is not validated here.
    /// </summary>
    public string? Status { get; init; }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.DeliveryId)
            .NotEmpty()
            .WithMessage(localizer.StockReservations_WebhookCallback_DeliveryId_NotEmpty);

        RuleFor(x => x.DeliveryId)
            .MaximumLength(DeliveryConstants.DeliveryIdMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture,
                localizer.StockReservations_WebhookCallback_DeliveryId_MaximumLength, DeliveryConstants.DeliveryIdMaxLength));

        RuleFor(x => x.ReservationId)
            .NotEmpty()
            .WithMessage(localizer.StockReservations_WebhookCallback_ReservationId_NotEmpty);

        RuleFor(x => x.ProviderReference)
            .NotEmpty()
            .WithMessage(localizer.StockReservations_WebhookCallback_ProviderReference_NotEmpty)
            .When(ConfirmsTheReservation);

        RuleFor(x => x.ProviderReference)
            .MaximumLength(ReservationConstants.ProviderReferenceMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture,
                localizer.StockReservations_WebhookCallback_ProviderReference_MaximumLength, ReservationConstants.ProviderReferenceMaxLength));
    }

    private static bool ConfirmsTheReservation(Request request)
    {
        return ProviderStatusMap.Resolve(request.Status, out var status) == ProviderStatusResolution.Mapped
               && status == ReservationStatus.Committed;
    }
}
