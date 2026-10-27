using System.Net.Http.Headers;
using System.Net.Http.Json;
using Inventory.Application.Gateway;
using Polly;

namespace Inventory.Infrastructure.Gateway;

/// <summary>
///     The JSON provider: JSON request bodies, a bearer token, and a flat <c>confirmationReference</c> in the answer. Paths
///     are resolved under the connection's base address. The reservation id is the release's reference and idempotency key:
///     the same id always gets the same confirmation reference, and the reconcile asks about that id alone.
///     <list type="table">
///         <listheader>
///             <term>Call</term>
///             <description>Request and answer</description>
///         </listheader>
///         <item>
///             <term>Release</term>
///             <description>
///                 <c>POST releases</c> with <c>{ "productReference", "quantity", "reservationId" }</c>. Answers 200
///                 <c>{ "confirmationReference" }</c>.
///             </description>
///         </item>
///         <item>
///             <term>Reconcile</term>
///             <description>
///                 <c>GET releases/{reservationId}/status</c>. 200 <c>{ "confirmationReference" }</c> means released. 404 with
///                 <c>{ "error": "release_not_found" }</c> means the warehouse knows no release for that reservation. Any other
///                 404 is the route missing.
///             </description>
///         </item>
///         <item>
///             <term>Push stock</term>
///             <description>
///                 <c>POST stock/levels</c> with <c>{ "items": [ { "productReference", "quantity" } ] }</c>. Answers 200
///                 <c>{ "confirmationReference" }</c>.
///             </description>
///         </item>
///     </list>
/// </summary>
internal sealed class JsonApiWarehouseGateway(
    HttpClient httpClient, Uri baseUrl, string apiCredential, ResiliencePipeline<HttpResponseMessage> pipeline)
    : WarehouseHttpGateway(httpClient, baseUrl, pipeline)
{
    private const string ReleaseNotFoundWord = "release_not_found";

    protected override HttpRequestMessage BuildReleaseRequest(string reservationReference, string productReference, int quantity)
    {
        return Authenticated(new HttpRequestMessage(HttpMethod.Post, Resolve("releases"))
        {
            Content = JsonContent.Create(new ReleaseRequestBody(productReference, quantity, reservationReference))
        });
    }

    protected override HttpRequestMessage BuildReconcileRequest(string reservationReference)
    {
        return Authenticated(new HttpRequestMessage(
            HttpMethod.Get, Resolve($"releases/{Uri.EscapeDataString(reservationReference)}/status")));
    }

    protected override async Task<bool> SaysNoSuchReleaseAsync(HttpContent content, CancellationToken cancellationToken)
    {
        var body = await content.ReadFromJsonAsync<ErrorResponseBody>(cancellationToken);
        return string.Equals(body?.Error, ReleaseNotFoundWord, StringComparison.Ordinal);
    }

    protected override HttpRequestMessage BuildPushStockRequest(IReadOnlyCollection<WarehouseStockQuantity> quantities)
    {
        return Authenticated(new HttpRequestMessage(HttpMethod.Post, Resolve("stock/levels"))
        {
            Content = JsonContent.Create(new PushStockRequestBody(
                [.. quantities.Select(q => new PushStockItem(q.ProductReference, q.Quantity))]))
        });
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

    private sealed record ReleaseRequestBody(string ProductReference, int Quantity, string ReservationId);

    private sealed record PushStockRequestBody(IReadOnlyList<PushStockItem> Items);

    private sealed record PushStockItem(string ProductReference, int Quantity);

    private sealed record ReleaseResponseBody(string? ConfirmationReference);

    private sealed record ErrorResponseBody(string? Error);
}
