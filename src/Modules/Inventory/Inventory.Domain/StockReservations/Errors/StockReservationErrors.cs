using System.Net;
using Common.Domain.ResultMonad;

namespace Inventory.Domain.StockReservations.Errors;

public static class StockReservationErrors
{
    public static readonly Error NotReserved = new() { Key = nameof(NotReserved) };
    public static readonly Error DeadlinePassed = new() { Key = nameof(DeadlinePassed) };
    public static readonly Error CannotRelease = new() { Key = nameof(CannotRelease) };

    /// <summary>
    ///     An earlier release attempt was interrupted between calling the external warehouse system and
    ///     recording the outcome; its true state at the provider is unknown, so a new attempt is refused
    ///     rather than risking a blind double-release.
    /// </summary>
    public static readonly Error AmbiguousPriorAttempt = new() { Key = nameof(AmbiguousPriorAttempt) };

    /// <summary>A release attempt is reconciled only while its marker stands: there is no attempt whose outcome is unknown.</summary>
    public static readonly Error NoReleaseAttempt = new() { Key = nameof(NoReleaseAttempt), StatusCode = HttpStatusCode.Conflict };

    /// <summary>
    ///     The provider reported a status this system has no mapping for, so the reservation no longer follows it and takes no
    ///     local command until someone resolves it by hand.
    /// </summary>
    public static readonly Error ReservationReadOnly = new()
    {
        Key = nameof(ReservationReadOnly),
        StatusCode = HttpStatusCode.Conflict
    };

    public static readonly Error ProviderReferenceRequired = new() { Key = nameof(ProviderReferenceRequired) };

    /// <summary>Only a read-only reservation is resolved by a person; a writable one takes the ordinary commands.</summary>
    public static readonly Error NotReadOnly = new() { Key = nameof(NotReadOnly), StatusCode = HttpStatusCode.Conflict };

    /// <summary>The chosen status is not one a read-only reservation can be resolved to from the status it has.</summary>
    public static readonly Error ResolutionNotAllowed = new() { Key = nameof(ResolutionNotAllowed) };

    /// <summary>
    ///     A webhook delivery is stamped further ahead of this system's clock than <c>WebhookMaxFutureSkewSeconds</c> allows.
    ///     Applied, it would make every later delivery of the reservation stale, so it is refused before anything is recorded.
    /// </summary>
    public static readonly Error WebhookOccurredOnInTheFuture = new() { Key = nameof(WebhookOccurredOnInTheFuture) };

    /// <summary>The inbound webhook carried no signature header.</summary>
    public static readonly Error MissingWebhookSignature = new()
    {
        Key = nameof(MissingWebhookSignature),
        StatusCode = HttpStatusCode.Unauthorized
    };

    /// <summary>The signature of the inbound webhook does not match the raw body under the connection's secret.</summary>
    public static readonly Error InvalidWebhookSignature = new()
    {
        Key = nameof(InvalidWebhookSignature),
        StatusCode = HttpStatusCode.Unauthorized
    };

    /// <summary>The authenticated webhook body is not the JSON shape the endpoint reads.</summary>
    public static readonly Error MalformedWebhookBody = new()
    {
        Key = nameof(MalformedWebhookBody),
        StatusCode = HttpStatusCode.BadRequest
    };

    public static readonly Error SeriesTooLong = new() { Key = nameof(SeriesTooLong) };

    public static readonly Error ProductNotFound = new() { Key = nameof(ProductNotFound) };

    public static readonly Error ProductStoreInactive = new() { Key = nameof(ProductStoreInactive) };
}
