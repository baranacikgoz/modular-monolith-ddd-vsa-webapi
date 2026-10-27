using System.Net.Http.Headers;
using System.Net.Http.Json;
using Polly;

namespace Inventory.Infrastructure.Gateway;

/// <summary>The JSON provider: JSON request bodies, a bearer token, and a flat <c>confirmationReference</c> in the answer.</summary>
internal sealed class JsonApiWarehouseGateway(
    HttpClient httpClient, Uri baseUrl, string apiCredential, ResiliencePipeline<HttpResponseMessage> pipeline)
    : WarehouseHttpGateway(httpClient, baseUrl, pipeline)
{
    protected override HttpRequestMessage BuildReleaseRequest(string productReference, int quantity)
    {
        return Authenticated(new HttpRequestMessage(HttpMethod.Post, Resolve("releases"))
        {
            Content = JsonContent.Create(new ReleaseRequestBody(productReference, quantity))
        });
    }

    protected override HttpRequestMessage BuildReconcileRequest(string productReference)
    {
        return Authenticated(new HttpRequestMessage(
            HttpMethod.Get, Resolve($"releases/{Uri.EscapeDataString(productReference)}/status")));
    }

    protected override async Task<string?> ReadReferenceAsync(HttpContent content, CancellationToken cancellationToken)
    {
        var body = await content.ReadFromJsonAsync<ReleaseResponseBody>(cancellationToken);
        return body?.ConfirmationReference;
    }

    private HttpRequestMessage Authenticated(HttpRequestMessage request)
    {
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiCredential);
        return request;
    }

    private sealed record ReleaseRequestBody(string ProductReference, int Quantity);

    private sealed record ReleaseResponseBody(string? ConfirmationReference);
}
