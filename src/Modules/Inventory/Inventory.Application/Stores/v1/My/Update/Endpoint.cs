using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using Inventory.Domain.Stores;
using Inventory.Domain.Stores.Specs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Application.Stores.v1.My.Update;
internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder myStoresApiGroup)
    {
        myStoresApiGroup
            .MapPatch("", UpdateMyStoreAsync)
            .WithDescription("Update my product.")
            .MustHavePermission(CustomActions.Create, CustomResources.Products)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async ValueTask<Result> UpdateMyStoreAsync(
        [FromBody] Request request,
        [FromServices] ICurrentUser currentUser,
        [FromServices] IRepository<Store> repository,
        [FromKeyedServices(nameof(Inventory))] IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
        => await repository
            .SingleOrDefaultAsResultAsync(new StoreByOwnerIdSpec(currentUser.Id), cancellationToken)
            .TapWhenAsync(store => store.UpdateName(request.Name!), when: _ => !string.IsNullOrEmpty(request.Name))
            .TapWhenAsync(store => store.UpdateDescription(request.Description!), when: _ => !string.IsNullOrEmpty(request.Description))
            .TapAsync(async _ => await unitOfWork.SaveChangesAsync(cancellationToken));
            
}