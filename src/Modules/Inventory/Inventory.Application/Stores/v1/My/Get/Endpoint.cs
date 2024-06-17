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
using Common.Domain.StronglyTypedIds;

namespace Inventory.Application.Stores.v1.My.Get;
internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder myStoresApiGroup)
    {
        myStoresApiGroup
            .MapGet("", GetMyStoreAsync)
            .WithDescription("Get my store.")
            .MustHavePermission(CustomActions.ReadMy, CustomResources.Stores)
            .Produces<Response>(StatusCodes.Status200OK)
            .TransformResultTo<Response>();
    }

    public class StoreWithProductCountByOwnerIdSpec : SingleResultSpecification<Store, Response>
    {
        public StoreWithProductCountByOwnerIdSpec(ApplicationUserId ownerId)
            => Query
                .Select(s => new Response(s.Id, s.Name, s.Description, s.LogoUrl, s.StoreProducts.Count))
                .Where(s => s.OwnerId == ownerId);
    }

    private static async Task<Result<Response>> GetMyStoreAsync(
        [FromServices] ICurrentUser currentUser,
        [FromServices] IRepository<Store> repository,
        CancellationToken cancellationToken)
        => await repository
            .SingleOrDefaultAsResultAsync(new StoreWithProductCountByOwnerIdSpec(currentUser.Id), cancellationToken);
}
