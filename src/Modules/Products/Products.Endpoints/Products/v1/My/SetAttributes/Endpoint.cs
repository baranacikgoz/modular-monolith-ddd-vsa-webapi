using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Options;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using Products.Application.Persistence;
using Products.Domain.Products;

namespace Products.Endpoints.Products.v1.My.SetAttributes;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder myProductsApiGroup)
    {
        myProductsApiGroup
            .MapPut("my/{id}/attributes", SetMyProductAttributesAsync)
            .WithDescription("Replace the whole attribute list of my product. An empty list clears it.")
            .RequireScope(KeycloakScopes.Products.UpdateOwn)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> SetMyProductAttributesAsync(
        [AsParameters] Request request,
        ICurrentUser currentUser,
        IProductsDbContext dbContext,
        IOptions<ProductsOptions> options,
        CancellationToken cancellationToken)
    {
        var attributes = request.Body.Attributes
            .Select(a => new ProductAttribute(a.Name.Trim(), a.Value.Trim()))
            .ToList();

        return await dbContext
            .Products
            .TagWith(nameof(SetMyProductAttributesAsync), request.Id)
            .Where(p => p.Store.OwnerId == currentUser.Id && p.Id == request.Id)
            .SingleAsResultAsync(nameof(Product), cancellationToken)
            .TapAsync(product => product.SetAttributes(attributes, options.Value.MaxAttributesPerProduct))
            .TapAsync(async _ => await dbContext.SaveChangesAsync(cancellationToken));
    }
}
