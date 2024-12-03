using Common.Application.Auth;
using Common.Domain.ResultMonad;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Common.Application.Extensions;
using Common.Application.Persistence;
using Inventory.Domain.Stores;
using Ardalis.Specification;
using Common.Application.ModelBinders;

namespace Inventory.Application.Stores.v1.Get;
internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder myStoresApiGroup)
    {
        myStoresApiGroup
            .MapGet("{id}", GetStoreAsync)
            .WithDescription("Get store.")
            .MustHavePermission(CustomActions.Read, CustomResources.Stores)
            .Produces<Response>(StatusCodes.Status200OK)
            .TransformResultTo<Response>();
    }

    private sealed class StoreWithProductCountByIdSpec : SingleResultSpecification<Store, Response>
    {
        public StoreWithProductCountByIdSpec(StoreId id)
            => Query
                .Select(s => new Response
                {
                    Id = s.Id,
                    OwnerId = s.OwnerId,
                    Name = s.Name,
                    Description = s.Description,
                    LogoUrl = s.LogoUrl,
                    ProductCount = s.StoreProducts.Count,
                    CreatedBy = s.CreatedBy,
                    CreatedOn = s.CreatedOn,
                    LastModifiedBy = s.LastModifiedBy,
                    LastModifiedOn = s.LastModifiedOn
                })
                .Where(s => s.Id == id);
    }

    private static async Task<Result<Response>> GetStoreAsync(
        [FromRoute, ModelBinder<StronglyTypedIdBinder<StoreId>>] StoreId id,
        [FromServices] IRepository<Store> repository,
        CancellationToken cancellationToken)
        => await repository
            .SingleOrDefaultAsResultAsync(new StoreWithProductCountByIdSpec(id), cancellationToken);
}
