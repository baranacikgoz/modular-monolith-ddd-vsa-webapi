using System.Globalization;
using System.Net.Http.Json;
using Polly;

namespace Inventory.Infrastructure.Gateway;

/// <summary>
///     The second provider behind the same abstraction: form encoded request bodies, an API key header, and the reference
///     nested in a <c>result</c> envelope. Same outcomes for the caller as the JSON provider, a different wire.
/// </summary>
internal sealed class FormApiWarehouseGateway(
    HttpClient httpClient, Uri baseUrl, string apiCredential, ResiliencePipeline<HttpResponseMessage> pipeline)
    : WarehouseHttpGateway(httpClient, baseUrl, pipeline)
{
    private const string ApiKeyHeaderName = "X-Api-Key";

    protected override HttpRequestMessage BuildReleaseRequest(string productReference, int quantity)
    {
        return Authenticated(new HttpRequestMessage(HttpMethod.Post, Resolve("v2/holds/release"))
        {
            Content = new FormUrlEncodedContent(
            [
                KeyValuePair.Create("sku", productReference),
                KeyValuePair.Create("units", quantity.ToString(CultureInfo.InvariantCulture))
            ])
        });
    }

    protected override HttpRequestMessage BuildReconcileRequest(string productReference)
    {
        return Authenticated(new HttpRequestMessage(
            HttpMethod.Get, Resolve($"v2/holds/release?sku={Uri.EscapeDataString(productReference)}")));
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

    private sealed record HoldResult(string? Reference);
}
