using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Common.Infrastructure.Security;
using Inventory.Application.Persistence;
using Inventory.Domain.WarehouseConnections;
using Inventory.Endpoints.StockReservations.v1.WebhookCallback;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Inventory.Endpoints.WarehouseConnections.v1.RotateWebhookSecret;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder warehouseConnectionsApiGroup)
    {
        warehouseConnectionsApiGroup
            .MapPost("{id}/rotate-webhook-secret", RotateWebhookSecretAsync)
            .WithDescription("Generate a new webhook secret. The response shows it once; the previous secret stops verifying immediately.")
            .RequireScope(KeycloakScopes.WarehouseConnections.Update)
            .Produces<Response>()
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> RotateWebhookSecretAsync(
        [AsParameters] Request request,
        IInventoryDbContext dbContext,
        SecretProtector protector,
        CancellationToken cancellationToken)
    {
        var webhookSecret = SecretGenerator.GenerateSecret();

        return await dbContext.WarehouseConnections
            .TagWith(nameof(RotateWebhookSecretAsync), request.Id)
            .Where(c => c.Id == request.Id)
            .SingleAsResultAsync(nameof(WarehouseConnection), cancellationToken)
            .TapAsync(connection => connection.RotateWebhookSecret(protector.Encrypt(webhookSecret)))
            .TapAsync(_ => dbContext.SaveChangesAsync(cancellationToken))
            .MapAsync(connection => new Response
            {
                WebhookSecret = webhookSecret,
                WebhookPath = WebhookRoute.PathFor(connection.Id)
            });
    }
}
