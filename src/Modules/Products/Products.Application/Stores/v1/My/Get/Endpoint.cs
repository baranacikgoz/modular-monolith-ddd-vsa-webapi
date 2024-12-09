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
using Products.Domain.Stores;

namespace Products.Application.Stores.v1.My.Get;
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

    private sealed class StoreWithProductCountByOwnerIdSpec : SingleResultSpecification<Store, Response>
    {
        public StoreWithProductCountByOwnerIdSpec(ApplicationUserId ownerId)
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
                .Where(s => s.OwnerId == ownerId);
    }

    private static async Task<Result<Response>> GetMyStoreAsync(
        [FromServices] ICurrentUser currentUser,
        [FromServices] IRepository<Store> repository,
        CancellationToken cancellationToken)
        => await repository
            .SingleOrDefaultAsResultAsync(new StoreWithProductCountByOwnerIdSpec(currentUser.Id), cancellationToken);
}
