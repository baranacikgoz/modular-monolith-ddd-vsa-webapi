using Ardalis.Specification;
using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using Inventory.Domain.Stores;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Inventory.Application.Stores.v1.My.Update;
internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder myStoresApiGroup)
    {
        myStoresApiGroup
            .MapPut("", UpdateMyStoreAsync)
            .WithDescription("Update my product.")
            .MustHavePermission(CustomActions.Create, CustomResources.Products)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private sealed class StoreByOwnerIdSpec : SingleResultSpecification<Store>
    {
        public StoreByOwnerIdSpec(ApplicationUserId ownerId)
            => Query
                .Where(s => s.OwnerId == ownerId);
    }

    private static async Task<Result> UpdateMyStoreAsync(
        [FromBody] Request request,
        [FromServices] ICurrentUser currentUser,
        [FromServices] IRepository<Store> repository,
        [FromKeyedServices(nameof(Inventory))] IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
        => await repository
            .SingleOrDefaultAsResultAsync(new StoreByOwnerIdSpec(currentUser.Id), cancellationToken)
            .TapAsync(store => store.Update(request.Name, request.Description))
            .TapAsync(async _ => await unitOfWork.SaveChangesAsync(cancellationToken));

}
