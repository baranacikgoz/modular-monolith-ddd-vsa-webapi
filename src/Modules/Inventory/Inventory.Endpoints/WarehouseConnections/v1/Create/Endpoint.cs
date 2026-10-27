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
using Microsoft.EntityFrameworkCore;

namespace Inventory.Endpoints.WarehouseConnections.v1.Create;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder warehouseConnectionsApiGroup)
    {
        warehouseConnectionsApiGroup
            .MapPost("", CreateWarehouseConnectionAsync)
            .WithDescription("Connect a store to its warehouse system. The response carries the webhook secret exactly once; the API credential and the secret are stored encrypted and never returned again. " +
                             "The Dummy provider needs no base address and no credential and is refused in Production.")
            .RequireScope(KeycloakScopes.WarehouseConnections.Create)
            .Produces<Response>()
            .TransformResultTo<Response>();
    }

    private static Task<Result<Response>> CreateWarehouseConnectionAsync(
        Request request,
        IInventoryDbContext dbContext,
        SecretProtector protector,
        CancellationToken cancellationToken)
    {
        return CreateAsync(request, dbContext, protector, cancellationToken);
    }

    /// <summary>
    ///     The connection of one store, shared by the admin route and the store owner's route (which only decides first who may
    ///     ask). A store has at most one active connection.
    /// </summary>
    internal static async Task<Result<Response>> CreateAsync(
        Request request,
        IInventoryDbContext dbContext,
        SecretProtector protector,
        CancellationToken cancellationToken)
    {
        var webhookSecret = SecretGenerator.GenerateSecret();

        return await dbContext.WarehouseConnections
            .AsNoTracking()
            .TagWith(nameof(CreateAsync), "GetActiveByStoreId", request.StoreId)
            .Where(c => c.StoreId == request.StoreId && c.Status == WarehouseConnectionStatus.Active)
            .AnyAsResultAsync(cancellationToken)
            .TapAsync(any => any ? Error.ViolatesUniqueConstraint(nameof(WarehouseConnection)) : Result.Success)
            .MapAsync(_ => WarehouseConnection.Create(
                request.StoreId,
                request.Provider,
                BaseUrlOf(request),
                protector.Encrypt(ApiCredentialOf(request)),
                protector.Encrypt(webhookSecret)))
            .TapAsync(connection => dbContext.WarehouseConnections.Add(connection))
            // The check above narrows the common case; the unique index on the active connection of a store is what is
            // race-safe when two requests pass it together.
            .BindAsync(_ => dbContext.SaveChangesDetectingUniqueViolationAsync(
                Error.ViolatesUniqueConstraint(nameof(WarehouseConnection)), cancellationToken))
            .MapAsync(connection => new Response
            {
                Id = connection.Id,
                WebhookSecret = webhookSecret,
                WebhookPath = WebhookRoute.PathFor(connection.Id)
            });
    }

    // The validator guarantees both are present for every provider but Dummy, which has neither and stores a placeholder.
    private static Uri BaseUrlOf(Request request)
    {
        return request.Provider == WarehouseProvider.Dummy ? new Uri(Constants.DummyBaseUrl) : request.BaseUrl!;
    }

    private static string ApiCredentialOf(Request request)
    {
        return request.Provider == WarehouseProvider.Dummy ? Constants.DummyApiCredential : request.ApiCredential!;
    }
}
