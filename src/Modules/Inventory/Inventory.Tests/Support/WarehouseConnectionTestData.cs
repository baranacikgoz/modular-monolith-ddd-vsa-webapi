using Common.Infrastructure.Security;
using Inventory.Application.Persistence;
using Inventory.Application.ProductSummaries;
using Inventory.Domain.StockReservations;
using Inventory.Domain.WarehouseConnections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Tests.Support;

internal static class WarehouseConnectionTestData
{
    public const string ApiCredential = "api-credential-in-clear";

    public static readonly Uri BaseUrl = new("https://warehouse.example-partner.local/api/");

    /// <summary>A connection saved through the aggregate. <c>WebhookSecret</c> is the clear text a caller would have been shown once.</summary>
    public static async Task<(WarehouseConnection Connection, string WebhookSecret)> SeedAsync(
        IServiceProvider services,
        DefaultIdType? storeId = null,
        WarehouseProvider provider = WarehouseProvider.JsonApi,
        bool deactivated = false,
        Uri? baseUrl = null)
    {
        await using var scope = services.CreateAsyncScope();
        var protector = scope.ServiceProvider.GetRequiredService<SecretProtector>();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();

        var webhookSecret = SecretGenerator.GenerateSecret();
        var connection = WarehouseConnection.Create(
            storeId ?? DefaultIdType.CreateVersion7(),
            provider,
            baseUrl ?? BaseUrl,
            protector.Encrypt(ApiCredential),
            protector.Encrypt(webhookSecret));
        if (deactivated)
        {
            connection.Deactivate();
        }

        db.WarehouseConnections.Add(connection);
        await db.SaveChangesAsync();

        return (connection, webhookSecret);
    }

    /// <summary>Saves the local copy of a product owned by <paramref name="storeId" />, the link from a product to the store's connection.</summary>
    public static async Task<DefaultIdType> SeedProductOfStoreAsync(IServiceProvider services, DefaultIdType storeId)
    {
        await using var scope = services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();

        var summary = ProductSummary.Create(DefaultIdType.CreateVersion7());
        summary.ApplyIfNewer(storeId, "Widget", 1);
        db.ProductSummaries.Add(summary);
        await db.SaveChangesAsync();

        return summary.ProductId;
    }

    /// <summary>Saves an active reservation of <paramref name="productId" />, optionally already committed and optionally read-only.</summary>
    public static async Task<StockReservation> SeedReservationAsync(
        IServiceProvider services, DefaultIdType productId, bool committed = false, bool readOnly = false,
        DateTimeOffset? deadline = null)
    {
        await using var scope = services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();

        var reservedUntil = deadline ?? DateTimeOffset.UtcNow.AddDays(5);
        var reservation = StockReservation.Reserve(productId, 10, reservedUntil);
        if (committed)
        {
            reservation.Commit("seeded-provider-reference", reservedUntil.AddMinutes(-1));
        }

        if (readOnly)
        {
            reservation.RecordUnrecognizedProviderStatus("seeded_unmapped_word", reservedUntil.AddMinutes(-1));
        }

        db.StockReservations.Add(reservation);
        await db.SaveChangesAsync();

        return reservation;
    }

    public static async Task<StockReservation> ReloadReservationAsync(IServiceProvider services, StockReservationId id)
    {
        await using var scope = services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        return await db.StockReservations.AsNoTracking().SingleAsync(r => r.Id == id);
    }

    public static async Task<WarehouseConnection> ReloadAsync(IServiceProvider services, WarehouseConnectionId id)
    {
        await using var scope = services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        return await db.WarehouseConnections.AsNoTracking().SingleAsync(c => c.Id == id);
    }

    public static string Decrypt(IServiceProvider services, byte[] encrypted)
    {
        return services.GetRequiredService<SecretProtector>().Decrypt(encrypted);
    }
}
