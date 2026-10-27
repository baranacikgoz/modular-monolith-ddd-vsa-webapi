using System.Net;
using System.Text.Json;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Resiliency;
using Inventory.Application.Gateway;
using Inventory.Domain.StockReservations;
using Polly;
using Polly.CircuitBreaker;
using Polly.RateLimiting;
using Polly.Timeout;

namespace Inventory.Infrastructure.Gateway;

/// <summary>
///     What every HTTP warehouse provider shares: sending through the connection's pipeline and turning the answer into a
///     <see cref="Result" />. A provider only states its wire shape (the request builders, where its response carries the
///     reference, and the words in which it says it knows no such release).
///     <para>
///         Every call goes through <paramref name="pipeline" />, the pipeline of this connection and provider
///         (<see cref="WarehouseResiliencePipelines" />). It has no retry, so the release, which is not idempotent, is never sent
///         twice by a retry; the reconcile call is a read and is simply asked again by whoever needs the answer. A stock push
///         is no different for the pipeline, though its caller may always send fresh quantities again. Our own quota
///         and an open circuit stop a call before anything is sent, which is a definite outcome and comes back as a failed
///         <see cref="Result" />. A timeout or a dropped connection says nothing about what the warehouse did: it surfaces as a
///         <see cref="WarehouseOutcomeUnknownException" /> and the caller reconciles.
///     </para>
/// </summary>
internal abstract class WarehouseHttpGateway(
    HttpClient httpClient, Uri baseUrl, ResiliencePipeline<HttpResponseMessage> pipeline) : IWarehouseGateway
{
    // A bare 404 always means "the route is not there". Only a reconcile can be told that the warehouse knows no such release,
    // and only when the body of the 404 says so in the provider's documented words.
    public Task<Result<string>> RequestReleaseAsync(
        StockReservationId reservationId, string productReference, int quantity, CancellationToken cancellationToken)
    {
        return SendAsync(
            BuildReleaseRequest(reservationId.Value.ToString(), productReference, quantity), canReportMissingRelease: false, cancellationToken);
    }

    public Task<Result<string>> ReconcileReleaseAsync(StockReservationId reservationId, CancellationToken cancellationToken)
    {
        return SendAsync(BuildReconcileRequest(reservationId.Value.ToString()), canReportMissingRelease: true, cancellationToken);
    }

    public Task<Result<string>> PushStockAsync(IReadOnlyCollection<WarehouseStockQuantity> quantities, CancellationToken cancellationToken)
    {
        return SendAsync(BuildPushStockRequest(quantities), canReportMissingRelease: false, cancellationToken);
    }

    /// <summary>
    ///     The release call. <paramref name="reservationReference" /> is the reservation id, the provider-facing reference and
    ///     idempotency key of the release.
    /// </summary>
    protected abstract HttpRequestMessage BuildReleaseRequest(string reservationReference, string productReference, int quantity);

    /// <summary>The read that asks what became of the release made under <paramref name="reservationReference" />.</summary>
    protected abstract HttpRequestMessage BuildReconcileRequest(string reservationReference);

    /// <summary>
    ///     True when the body of a 404 to a reconcile says, in the provider's documented words, that the warehouse knows no
    ///     release under the reference. An empty body, another shape or another word is false: that 404 is the route missing.
    /// </summary>
    protected abstract Task<bool> SaysNoSuchReleaseAsync(HttpContent content, CancellationToken cancellationToken);

    protected abstract HttpRequestMessage BuildPushStockRequest(IReadOnlyCollection<WarehouseStockQuantity> quantities);

    /// <summary>The confirmation reference in a successful answer, or null when the body does not carry one.</summary>
    protected abstract Task<string?> ReadReferenceAsync(HttpContent content, CancellationToken cancellationToken);

    /// <summary>The address of <paramref name="relativePath" /> under the connection's base address, with or without a trailing slash on it.</summary>
    protected Uri Resolve(string relativePath)
    {
#pragma warning disable S1075 // A URI segment separator, not a file system path.
        var root = baseUrl.AbsoluteUri.EndsWith('/') ? baseUrl : new Uri(baseUrl.AbsoluteUri + '/');
#pragma warning restore S1075
        return new Uri(root, relativePath);
    }

    private async Task<Result<string>> SendAsync(HttpRequestMessage request, bool canReportMissingRelease, CancellationToken cancellationToken)
    {
        using (request)
        {
            HttpResponseMessage response;
            try
            {
                response = await httpClient.SendWithPipelineAsync(request, pipeline, cancellationToken);
            }
            catch (RateLimiterRejectedException ex)
            {
                return WarehouseGatewayErrors.Throttled(WholeSeconds(ex.RetryAfter));
            }
            catch (BrokenCircuitException)
            {
                return WarehouseGatewayErrors.CircuitOpen;
            }
            catch (HttpRequestException ex) when (WarehouseAddressRefusedException.IsCauseOf(ex))
            {
                // The connect guard refused the host before a byte was sent: a definite outcome, not an unknown one.
                return WarehouseGatewayErrors.AddressNotAllowed;
            }
            catch (Exception ex) when (IsOutcomeUnknown(ex, cancellationToken))
            {
                throw new WarehouseOutcomeUnknownException("The warehouse system did not answer in time.", ex);
            }

            using (response)
            {
                return await MapResponseAsync(response, canReportMissingRelease, cancellationToken);
            }
        }
    }

    private async Task<Result<string>> MapResponseAsync(
        HttpResponseMessage response, bool canReportMissingRelease, CancellationToken cancellationToken)
    {
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return canReportMissingRelease && await SaysNoSuchReleaseSafelyAsync(response.Content, cancellationToken)
                ? WarehouseGatewayErrors.ReferenceNotFound
                : WarehouseGatewayErrors.EndpointNotFound;
        }

        if (response.StatusCode == HttpStatusCode.TooManyRequests)
        {
            return WarehouseGatewayErrors.Throttled(WholeSeconds(RetryAfterOf(response)));
        }

        if (!response.IsSuccessStatusCode)
        {
            return WarehouseGatewayErrors.CallFailed(response.StatusCode);
        }

        string? reference;
        try
        {
            reference = await ReadReferenceAsync(response.Content, cancellationToken);
        }
        catch (JsonException)
        {
            reference = null;
        }

        return string.IsNullOrEmpty(reference)
            ? WarehouseGatewayErrors.MalformedResponse
            : Result<string>.Success(reference);
    }

    /// <summary>A body that is not the documented JSON is not the provider saying anything: it is a plain 404.</summary>
    private async Task<bool> SaysNoSuchReleaseSafelyAsync(HttpContent content, CancellationToken cancellationToken)
    {
        try
        {
            return await SaysNoSuchReleaseAsync(content, cancellationToken);
        }
        catch (JsonException)
        {
            return false;
        }
    }

    /// <summary>A caller that cancelled is not an unknown outcome; the client's own overall timeout and the attempt timeout are.</summary>
    private static bool IsOutcomeUnknown(Exception exception, CancellationToken cancellationToken)
    {
        return exception is HttpRequestException or TimeoutRejectedException
               || (exception is TaskCanceledException && !cancellationToken.IsCancellationRequested);
    }

    private static TimeSpan? RetryAfterOf(HttpResponseMessage response)
    {
        var header = response.Headers.RetryAfter;
        if (header?.Delta is { } delta)
        {
            return delta;
        }

        if (header?.Date is { } date)
        {
            var wait = date - DateTimeOffset.UtcNow;
            return wait > TimeSpan.Zero ? wait : TimeSpan.Zero;
        }

        return null;
    }

    private static int? WholeSeconds(TimeSpan? wait)
    {
        return wait is { } value ? (int)Math.Max(1, Math.Ceiling(value.TotalSeconds)) : null;
    }
}
