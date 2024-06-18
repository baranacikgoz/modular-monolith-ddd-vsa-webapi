using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Common.Application.Auth;
using Common.Domain.ResultMonad;
using Common.Application.Extensions;
using Common.Application.Persistence;
using Inventory.Domain.Stores;
using Microsoft.Extensions.DependencyInjection;
using Ardalis.Specification;
using Common.Domain.StronglyTypedIds;

namespace Inventory.Application.Stores.v1.My.Create;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder myStoresApiGroup)
    {
        myStoresApiGroup
            .MapPost("", CreateMyStoreAsync)
            .WithDescription("Create my store.")
            .MustHavePermission(CustomActions.CreateMy, CustomResources.Stores)
            .Produces<Response>(StatusCodes.Status200OK)
            .TransformResultTo<Response>();
    }

    private sealed class StoreByOwnerIdSpec : SingleResultSpecification<Store>
    {
        public StoreByOwnerIdSpec(ApplicationUserId ownerId)
            => Query
                .Where(s => s.OwnerId == ownerId);
    }

    private static async Task<Result<Response>> CreateMyStoreAsync(
        [FromBody] Request request,
        [FromServices] ICurrentUser currentUser,
        [FromServices] IRepository<Store> repository,
        [FromKeyedServices(nameof(Inventory))] IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
        => await repository.AnyAsyncAsResult(new StoreByOwnerIdSpec(currentUser.Id), cancellationToken)
            .TapAsync(any => any ? Error.ViolatesUniqueConstraint(nameof(Store)) : Result.Success)
            .BindAsync(_ => Store.Create(currentUser.Id, request.Name, request.Description))
            .TapAsync(store => repository.Add(store))
            .TapAsync(async _ => await unitOfWork.SaveChangesAsync(cancellationToken))
            .MapAsync(store => new Response(store.Id));
}
