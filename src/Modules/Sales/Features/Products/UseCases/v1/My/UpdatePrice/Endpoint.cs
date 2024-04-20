using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Common.Core.Auth;
using Common.Core.Contracts.Results;
using Microsoft.AspNetCore.Mvc;
using Common.Core.Extensions;
using Sales.Persistence;
using Sales.Features.Products.Domain;
using Microsoft.EntityFrameworkCore;
using Sales.Features.Stores.Domain;
using Common.Core.ModelBinders;

namespace Sales.Features.Products.UseCases.v1.My.UpdatePrice;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder salesApiGroup)
    {
        salesApiGroup
            .MapPatch("{storeId}/{productId}/update-price", UpdateMyProductsPriceAsync)
            .WithDescription("Create a product.")
            .MustHavePermission(CustomActions.UpdateMy, CustomResources.Products)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async ValueTask<Result> UpdateMyProductsPriceAsync(
        [FromRoute, ModelBinder<StronglyTypedIdBinder<StoreId>>] StoreId storeId,
        [FromRoute, ModelBinder<StronglyTypedIdBinder<ProductId>>] ProductId productId,
        [FromBody] Request request,
        [FromServices] SalesDbContext dbContext,
        [FromServices] ICurrentUser currentUser,
        CancellationToken cancellationToken)
        => await Result<bool>
            .CreateAsync(async () => await dbContext
                                          .Stores
                                          .AnyAsync(s => s.Id == storeId && s.OwnerId == currentUser.Id, cancellationToken))
            .TapAsync(ownedByCurrentUser => ownedByCurrentUser ? Result.Success : Error.NotOwned(nameof(Store), storeId))
            .MapAsync(async _ => await dbContext
                                       .Products
                                       .Where(p => p.StoreId == storeId && p.Id == productId)
                                       .SingleOrDefaultAsync(cancellationToken))
            .TapAsync(product => product is null ? Error.NotFound(nameof(Product), productId) : Result.Success)
            .TapAsync(product => product!.UpdatePrice(request.Price))
            .MapAsync(_ => Result.Success);
}
