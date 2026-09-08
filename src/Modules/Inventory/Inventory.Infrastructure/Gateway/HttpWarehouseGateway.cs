using System.Net;
using System.Net.Http.Json;
using Common.Domain.ResultMonad;
using Inventory.Application.Gateway;

namespace Inventory.Infrastructure.Gateway;

internal sealed class HttpWarehouseGateway(HttpClient httpClient) : IWarehouseGateway
{
    public async Task<Result<string>> RequestReleaseAsync(string productReference, int quantity, CancellationToken cancellationToken)
    {
        using var response = await httpClient.PostAsJsonAsync(
            "releases",
            new ReleaseRequestBody(productReference, quantity),
            cancellationToken);

        return await MapReferenceResponseAsync(response, cancellationToken);
    }

    public async Task<Result<string>> ReconcileReleaseAsync(string productReference, CancellationToken cancellationToken)
    {
        using var response = await httpClient.GetAsync(
            new Uri($"releases/{Uri.EscapeDataString(productReference)}/status", UriKind.Relative),
            cancellationToken);

        return await MapReferenceResponseAsync(response, cancellationToken);
    }

    private static async Task<Result<string>> MapReferenceResponseAsync(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return Error.NotFound(nameof(ReleaseResponseBody));
        }

        if (!response.IsSuccessStatusCode)
        {
            return new Error
            {
                Key = "WarehouseGatewayCallFailed",
                Value = (int)response.StatusCode,
                StatusCode = HttpStatusCode.BadGateway
            };
        }

        var body = await response.Content.ReadFromJsonAsync<ReleaseResponseBody>(cancellationToken);
        return string.IsNullOrEmpty(body?.ConfirmationReference)
            ? new Error { Key = "WarehouseGatewayMalformedResponse", StatusCode = HttpStatusCode.BadGateway }
            : Result<string>.Success(body.ConfirmationReference);
    }

    private sealed record ReleaseRequestBody(string ProductReference, int Quantity);

    private sealed record ReleaseResponseBody(string ConfirmationReference);
}
