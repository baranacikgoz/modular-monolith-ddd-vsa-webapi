using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.ModelBinders;
using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using Inventory.Domain.StoreProducts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Inventory.Application.StoreProducts.v1.Get;
internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder myStoresApiGroup)
    {
        myStoresApiGroup
            .MapGet("{id}", GetStoreProductAsync)
            .WithDescription("Get StoreProduct.")
            .MustHavePermission(CustomActions.Read, CustomResources.StoreProducts)
            .Produces<Response>(StatusCodes.Status200OK)
            .TransformResultTo<Response>();
    }

    private sealed class StoreProductByIdSpec : SingleResultSpecification<StoreProduct, Response>
    {
        public StoreProductByIdSpec(StoreProductId id)
            => Query
                .Select(sp => new Response(sp.Id, sp.Product.Name, sp.Product.Description))
                .Include(sp => sp.Product)
                .Where(sp => sp.Id == id);
    }

    private static async Task<Result<Response>> GetStoreProductAsync(
        [FromRoute, ModelBinder<StronglyTypedIdBinder<StoreProductId>>] StoreProductId id,
        [FromServices] IRepository<StoreProduct> repository,
        CancellationToken cancellationToken)
        => await repository
            .SingleOrDefaultAsResultAsync(new StoreProductByIdSpec(id), cancellationToken);
}
