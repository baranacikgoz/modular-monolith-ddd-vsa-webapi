using Common.Infrastructure.Security;
using Inventory.Application.Persistence;
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
