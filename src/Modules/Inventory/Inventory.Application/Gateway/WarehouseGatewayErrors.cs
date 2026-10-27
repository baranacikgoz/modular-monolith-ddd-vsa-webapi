using System.Net;
using Common.Domain.ResultMonad;

namespace Inventory.Application.Gateway;

/// <summary>What a gateway call reports as a definite outcome. A timeout or a dropped connection is not here: it is ambiguous and surfaces as an exception.</summary>
public static class WarehouseGatewayErrors
{
    /// <summary>The warehouse answered with a status that is not a success; <paramref name="statusCode" /> is its answer.</summary>
    public static Error CallFailed(HttpStatusCode statusCode)
    {
        return new Error { Key = "WarehouseGatewayCallFailed", Value = (int)statusCode, StatusCode = HttpStatusCode.BadGateway };
    }

    /// <summary>
    ///     The warehouse answered 404 without saying that it knows no such release: the route itself does not exist there, which
    ///     points at the connection's base address, not at a missing reference. Every 404 of a release or a stock push is this,
    ///     and so is a bare 404 of a reconcile, which therefore changes nothing and leaves the attempt marker in place.
    /// </summary>
    public static readonly Error EndpointNotFound = new()
    {
        Key = "WarehouseGatewayEndpointNotFound",
        StatusCode = HttpStatusCode.BadGateway
    };

    /// <summary>
    ///     The connection's host is, or resolves to, a loopback, private or otherwise non public address, so the connection was
    ///     refused before anything was sent. A definite outcome: the call did not happen.
    /// </summary>
    public static readonly Error AddressNotAllowed = new()
    {
        Key = "WarehouseGatewayAddressNotAllowed",
        StatusCode = HttpStatusCode.BadGateway
    };

    /// <summary>
    ///     The warehouse answered a reconcile with a 404 whose body says, in the provider's documented words, that it knows no
    ///     release for the reservation. Only this lets the caller drop the attempt marker.
    /// </summary>
    public static readonly Error ReferenceNotFound = Error.NotFound("WarehouseReference");

    /// <summary>The warehouse answered successfully but not in the shape its provider documents.</summary>
    public static readonly Error MalformedResponse = new()
    {
        Key = "WarehouseGatewayMalformedResponse",
        StatusCode = HttpStatusCode.BadGateway
    };

    /// <summary>
    ///     Our own quota for the connection, or the warehouse's 429, stopped the call. Nothing was processed, so it is safe to
    ///     send again after <paramref name="retryAfterSeconds" /> (null when no wait is known).
    /// </summary>
    public static Error Throttled(int? retryAfterSeconds)
    {
        return new Error { Key = "WarehouseGatewayThrottled", Value = retryAfterSeconds, StatusCode = HttpStatusCode.TooManyRequests };
    }

    /// <summary>
    ///     What a caller answers with after a <see cref="WarehouseOutcomeUnknownException" />: the release may or may not have
    ///     happened, and the attempt marker stays until it is reconciled.
    /// </summary>
    public static readonly Error OutcomeUnknown = new()
    {
        Key = "WarehouseGatewayOutcomeUnknown",
        StatusCode = HttpStatusCode.GatewayTimeout
    };

    /// <summary>The connection's circuit is open after repeated failures: the call was not sent.</summary>
    public static readonly Error CircuitOpen = new()
    {
        Key = "WarehouseGatewayCircuitOpen",
        StatusCode = HttpStatusCode.ServiceUnavailable
    };
}
