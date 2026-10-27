using System.Security.Cryptography;
using System.Text;
using Inventory.Domain.StockReservations;
using Inventory.Domain.WarehouseConnections;

namespace Inventory.Tests.Support;

/// <summary>Posts a signed warehouse webhook the way the warehouse system would.</summary>
internal static class WarehouseWebhookSender
{
    public const string SignatureHeader = "X-Warehouse-Signature";

    public static Uri RouteOf(WarehouseConnectionId connectionId)
    {
        return new Uri($"/v1/stock-reservations/webhooks/warehouse/{connectionId}", UriKind.Relative);
    }

    public static string BodyOf(StockReservationId reservationId, string deliveryId, string providerReference)
    {
        return $$"""{"ReservationId":"{{reservationId.Value}}","ProviderReference":"{{providerReference}}","DeliveryId":"{{deliveryId}}"}""";
    }

    public static string Sign(string rawBody, string secret)
    {
        return Convert.ToHexString(HMACSHA256.HashData(Encoding.UTF8.GetBytes(secret), Encoding.UTF8.GetBytes(rawBody)));
    }

    public static async Task<HttpResponseMessage> PostAsync(
        HttpClient client,
        WarehouseConnectionId connectionId,
        string secret,
        StockReservationId reservationId,
        string deliveryId,
        string providerReference,
        string? signatureOverride = null)
    {
        return await PostRawAsync(client, connectionId, secret, BodyOf(reservationId, deliveryId, providerReference), signatureOverride);
    }

    public static async Task<HttpResponseMessage> PostRawAsync(
        HttpClient client, WarehouseConnectionId connectionId, string secret, string rawBody, string? signatureOverride = null)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, RouteOf(connectionId))
        {
            Content = new StringContent(rawBody, Encoding.UTF8, "application/json")
        };
        request.Headers.Add(SignatureHeader, signatureOverride ?? Sign(rawBody, secret));

        return await client.SendAsync(request);
    }
}
