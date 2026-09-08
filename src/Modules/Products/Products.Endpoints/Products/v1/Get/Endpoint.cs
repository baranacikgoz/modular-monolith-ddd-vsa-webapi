using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Inventory;
using Common.Infrastructure.Persistence.Extensions;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Products.Application.Persistence;
using Products.Domain.Products;

namespace Products.Endpoints.Products.v1.Get;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder productsApiGroup)
    {
        productsApiGroup
            .MapGet("{id}", GetProductAsync)
            .WithDescription("Get Product.")
            .RequireScope(KeycloakScopes.Products.View)
            .TransformResultTo<Response>()
            .Produces<Response>();
    }

    private static async Task<Result<Response>> GetProductAsync(
        [AsParameters] Request request,
        IProductsDbContext dbContext,
        IInterModuleRequestClient<GetStockLevelRequest, GetStockLevelResponse> stockLevelClient,
        CancellationToken cancellationToken)
    {
        return await dbContext
            .Products
            .AsNoTracking()
            .TagWith(nameof(GetProductAsync), request.Id)
            .Where(p => p.Id == request.Id)
            .Select(p => new Response
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Quantity = p.Quantity,
                Price = p.Price,
                CreatedBy = p.CreatedBy,
                CreatedOn = p.CreatedOn,
                LastModifiedBy = p.LastModifiedBy,
                LastModifiedOn = p.LastModifiedOn
            })
            .SingleAsResultAsync(nameof(Product), cancellationToken)
            // Cross-module sync call to Inventory (MassTransit request/response) to enrich the read
            // with live availability - Products never queries Inventory's tables directly.
            .CombineAsync(async response => Result<GetStockLevelResponse>.Success(
                await GetStockLevelSafelyAsync(stockLevelClient, response.Id.Value, cancellationToken)))
            .MapAsync(t => t.Item1 with { AvailableQuantity = t.Item2.AvailableQuantity });
    }

    /// <summary>
    ///     Zero-trust: Inventory may be a disabled module, mid-deploy, or genuinely down in this
    ///     environment. AvailableQuantity is an enrichment, not the resource itself - a Product read must
    ///     never fail (or hang for the full request timeout with no useful outcome) just because an optional
    ///     cross-module call didn't answer. Degrades to 0/0 instead of propagating the fault.
    /// </summary>
    private static async Task<GetStockLevelResponse> GetStockLevelSafelyAsync(
        IInterModuleRequestClient<GetStockLevelRequest, GetStockLevelResponse> stockLevelClient,
        DefaultIdType productId,
        CancellationToken cancellationToken)
    {
        try
        {
            return await stockLevelClient.SendAsync(new GetStockLevelRequest(productId), cancellationToken);
        }
        catch (RequestTimeoutException)
        {
            return new GetStockLevelResponse(0, 0);
        }
    }
}
