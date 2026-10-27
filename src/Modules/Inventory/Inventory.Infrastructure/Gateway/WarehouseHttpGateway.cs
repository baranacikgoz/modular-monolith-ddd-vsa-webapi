using System.Net;
using System.Text.Json;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Resiliency;
using Inventory.Application.Gateway;
using Polly;
using Polly.CircuitBreaker;
using Polly.RateLimiting;
using Polly.Timeout;

namespace Inventory.Infrastructure.Gateway;

/// <summary>
///     What every HTTP warehouse provider shares: sending through the connection's pipeline and turning the answer into a
///     <see cref="Result" />. A provider only states its wire shape (the two request builders and where its response
///     carries the reference).
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
    public Task<Result<string>> RequestReleaseAsync(string productReference, int quantity, CancellationToken cancellationToken)
    {
        return SendAsync(BuildReleaseRequest(productReference, quantity), cancellationToken);
    }

    public Task<Result<string>> ReconcileReleaseAsync(string productReference, CancellationToken cancellationToken)
    {
        return SendAsync(BuildReconcileRequest(productReference), cancellationToken);
    }

    public Task<Result<string>> PushStockAsync(IReadOnlyCollection<WarehouseStockQuantity> quantities, CancellationToken cancellationToken)
    {
        return SendAsync(BuildPushStockRequest(quantities), cancellationToken);
    }

    protected abstract HttpRequestMessage BuildReleaseRequest(string productReference, int quantity);

    protected abstract HttpRequestMessage BuildReconcileRequest(string productReference);

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

    private async Task<Result<string>> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
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
            catch (Exception ex) when (IsOutcomeUnknown(ex, cancellationToken))
            {
                throw new WarehouseOutcomeUnknownException("The warehouse system did not answer in time.", ex);
            }

            using (response)
            {
                return await MapResponseAsync(response, cancellationToken);
            }
        }
    }

    private async Task<Result<string>> MapResponseAsync(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return Error.NotFound("WarehouseReference");
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
