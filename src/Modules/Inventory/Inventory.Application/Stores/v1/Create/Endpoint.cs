using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Common.Application.Auth;
using Common.Domain.ResultMonad;
using Common.Application.Extensions;
using Common.Domain.StronglyTypedIds;
using Ardalis.Specification;
using Inventory.Domain.Stores;
using Microsoft.Extensions.DependencyInjection;
using Common.Application.Persistence;

namespace Inventory.Application.Stores.v1.Create;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder myStoresApiGroup)
    {
        myStoresApiGroup
            .MapPost("", CreateStoreAsync)
            .WithDescription("Create a store.")
            .MustHavePermission(CustomActions.Create, CustomResources.Stores)
            .Produces<Response>(StatusCodes.Status200OK)
            .TransformResultTo<Response>();
    }

    private sealed class StoreByOwnerIdSpec : SingleResultSpecification<Store>
    {
        public StoreByOwnerIdSpec(ApplicationUserId ownerId)
            => Query
                .Where(s => s.OwnerId == ownerId);
    }

    private static async Task<Result<Response>> CreateStoreAsync(
        [FromBody] Request request,
        [FromServices] IRepository<Store> repository,
        [FromKeyedServices(nameof(Inventory))] IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
        => await repository.AnyAsyncAsResult(new StoreByOwnerIdSpec(request.OwnerId), cancellationToken)
            .TapAsync(any => any ? Error.ViolatesUniqueConstraint(nameof(Store)) : Result.Success)
            .BindAsync(_ => Store.Create(request.OwnerId, request.Name, request.Description))
            .TapAsync(store => repository.Add(store))
            .TapAsync(async _ => await unitOfWork.SaveChangesAsync(cancellationToken))
            .MapAsync(store => new Response { Id = store.Id });
}
