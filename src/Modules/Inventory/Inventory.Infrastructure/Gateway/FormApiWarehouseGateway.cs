using System.Globalization;
using System.Net.Http.Json;
using Inventory.Application.Gateway;
using Polly;

namespace Inventory.Infrastructure.Gateway;

/// <summary>
///     The second provider behind the same abstraction: form encoded request bodies, an API key header, and the reference
///     nested in a <c>result</c> envelope. Same outcomes for the caller as the JSON provider, a different wire. Paths are
///     resolved under the connection's base address. The reservation id travels as <c>ref</c>, the release's reference and
///     idempotency key: the same <c>ref</c> always gets the same reference back, and the reconcile asks about that one alone.
///     <list type="table">
///         <listheader>
///             <term>Call</term>
///             <description>Request and answer</description>
///         </listheader>
///         <item>
///             <term>Release</term>
///             <description>
///                 <c>POST v2/holds/release</c> with form fields <c>sku</c>, <c>units</c> and <c>ref</c>. Answers 200
///                 <c>{ "result": { "reference" } }</c>.
///             </description>
///         </item>
///         <item>
///             <term>Reconcile</term>
///             <description>
///                 <c>GET v2/holds/release?ref={reservationId}</c>. 200 <c>{ "result": { "reference" } }</c> means released. 404
///                 with <c>{ "result": { "error": "hold_not_found" } }</c> means the warehouse knows no release for that
///                 reservation. Any other 404 is the route missing.
///             </description>
///         </item>
///         <item>
///             <term>Push stock</term>
///             <description>
///                 <c>POST v2/stock/sync</c> with form fields <c>sku[i]</c> and <c>units[i]</c>. Answers 200
///                 <c>{ "result": { "reference" } }</c>.
///             </description>
///         </item>
///     </list>
/// </summary>
internal sealed class FormApiWarehouseGateway(
    HttpClient httpClient, Uri baseUrl, string apiCredential, ResiliencePipeline<HttpResponseMessage> pipeline)
    : WarehouseHttpGateway(httpClient, baseUrl, pipeline)
{
    private const string ApiKeyHeaderName = "X-Api-Key";
    private const string HoldNotFoundWord = "hold_not_found";

    protected override HttpRequestMessage BuildReleaseRequest(string reservationReference, string productReference, int quantity)
    {
        return Authenticated(new HttpRequestMessage(HttpMethod.Post, Resolve("v2/holds/release"))
        {
            Content = new FormUrlEncodedContent(
            [
                KeyValuePair.Create("sku", productReference),
                KeyValuePair.Create("units", quantity.ToString(CultureInfo.InvariantCulture)),
                KeyValuePair.Create("ref", reservationReference)
            ])
        });
    }

    protected override HttpRequestMessage BuildReconcileRequest(string reservationReference)
    {
        return Authenticated(new HttpRequestMessage(
            HttpMethod.Get, Resolve($"v2/holds/release?ref={Uri.EscapeDataString(reservationReference)}")));
    }

    protected override async Task<bool> SaysNoSuchReleaseAsync(HttpContent content, CancellationToken cancellationToken)
    {
        var envelope = await content.ReadFromJsonAsync<HoldEnvelope>(cancellationToken);
        return string.Equals(envelope?.Result?.Error, HoldNotFoundWord, StringComparison.Ordinal);
    }

    protected override HttpRequestMessage BuildPushStockRequest(IReadOnlyCollection<WarehouseStockQuantity> quantities)
    {
        return Authenticated(new HttpRequestMessage(HttpMethod.Post, Resolve("v2/stock/sync"))
        {
            Content = new FormUrlEncodedContent(quantities.SelectMany((q, index) => new[]
            {
                KeyValuePair.Create($"sku[{index}]", q.ProductReference),
                KeyValuePair.Create($"units[{index}]", q.Quantity.ToString(CultureInfo.InvariantCulture))
            }))
        });
    }

    protected override async Task<string?> ReadReferenceAsync(HttpContent content, CancellationToken cancellationToken)
    {
        var envelope = await content.ReadFromJsonAsync<HoldEnvelope>(cancellationToken);
        return envelope?.Result?.Reference;
    }

    private HttpRequestMessage Authenticated(HttpRequestMessage request)
    {
        request.Headers.Add(ApiKeyHeaderName, apiCredential);
        return request;
    }

    private sealed record HoldEnvelope(HoldResult? Result);

    private sealed record HoldResult(string? Reference, string? Error);
}
