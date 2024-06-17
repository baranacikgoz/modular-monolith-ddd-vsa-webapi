using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Application.Auth;
using Common.Domain.ResultMonad;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Common.Application.Extensions;
using Common.Application.Persistence;
using Inventory.Domain.Stores;
using Inventory.Application.Stores.Specs;

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

    private static async Task<Result<Response>> GetMyStoreAsync(
        [FromServices] ICurrentUser currentUser,
        [FromServices] IRepository<Store> repository,
        CancellationToken cancellationToken)
        => await repository
            .SingleOrDefaultAsResultAsync(new StoreByOwnerIdSpec(currentUser.Id), cancellationToken)
            .MapAsync(store => new Response(store.Id, store.Name, store.Description, store.LogoUrl));
}
