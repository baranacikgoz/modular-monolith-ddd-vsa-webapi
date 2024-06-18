using Common.Application.Auth;
using Common.Domain.ResultMonad;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Common.Application.Extensions;
using Common.Application.Persistence;
using Ardalis.Specification;
using Common.Domain.StronglyTypedIds;
using Inventory.Domain.StoreProducts;
using Common.Application.ModelBinders;

namespace Inventory.Application.StoreProducts.v1.My.Get;
internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder myStoresApiGroup)
    {
        myStoresApiGroup
            .MapGet("{id}", GetMyProductAsync)
            .WithDescription("Get my product.")
            .MustHavePermission(CustomActions.ReadMy, CustomResources.StoreProducts)
            .Produces<Response>(StatusCodes.Status200OK)
            .TransformResultTo<Response>();
    }

    private sealed class StoreProductByStoreOwnerIdAndStoreProductIdSpec : SingleResultSpecification<StoreProduct, Response>
    {
        public StoreProductByStoreOwnerIdAndStoreProductIdSpec(ApplicationUserId ownerId, StoreProductId storeProductId)
            => Query
                .Select(sp => new Response(sp.Id, sp.Product.Name, sp.Product.Description))
                .Include(sp => sp.Product)
                .Where(sp => sp.Store.OwnerId == ownerId && sp.Id == storeProductId);
    }

    private static async Task<Result<Response>> GetMyProductAsync(
        [FromRoute, ModelBinder<StronglyTypedIdBinder<StoreProductId>>] StoreProductId id,
        [FromServices] ICurrentUser currentUser,
        [FromServices] IRepository<StoreProduct> repository,
        CancellationToken cancellationToken)
        => await repository
            .SingleOrDefaultAsResultAsync(new StoreProductByStoreOwnerIdAndStoreProductIdSpec(currentUser.Id, id), cancellationToken);
}
