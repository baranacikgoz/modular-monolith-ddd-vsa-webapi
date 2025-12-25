using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Products.Application.Persistence;

namespace Products.Endpoints.Products.v1.My.Get;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder myProductsApiGroup)
    {
        myProductsApiGroup
            .MapGet("my/{id}", GetMyProductAsync)
            .WithDescription("Get my product.")
            .MustHavePermission(CustomActions.ReadMy, CustomResources.Products)
            .Produces<Response>()
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> GetMyProductAsync(
        [AsParameters] Request request,
        [FromServices] ICurrentUser currentUser,
        [FromServices] IProductsDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext
            .Products
            .TagWith(nameof(GetMyProductAsync), request.Id)
            .Where(p => p.Store.OwnerId == currentUser.Id && p.Id == request.Id)
            .Select(p => new Response
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Quantity = p.Quantity,
                CreatedBy = p.CreatedBy,
                CreatedOn = p.CreatedOn,
                LastModifiedBy = p.LastModifiedBy,
                LastModifiedOn = p.LastModifiedOn
            })
            .SingleAsResultAsync(cancellationToken);
    }
}
