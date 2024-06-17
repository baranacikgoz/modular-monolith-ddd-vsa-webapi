using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Common.Application.Auth;
using Common.Domain.ResultMonad;
using Common.Application.Extensions;
using Common.Application.ModelBinders;
using Ardalis.Specification;
using Inventory.Domain.Stores;
using Microsoft.Extensions.DependencyInjection;
using Common.Application.Persistence;

namespace Inventory.Application.Stores.v1.Update;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder myStoresApiGroup)
    {
        myStoresApiGroup
            .MapPut("{id}", CreateStoreAsync)
            .WithDescription("Update a store.")
            .MustHavePermission(CustomActions.Update, CustomResources.Stores)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private sealed class StoreByIdSpec : SingleResultSpecification<Store>
    {
        public StoreByIdSpec(StoreId id)
            => Query
                .Where(s => s.Id == id);
    }

    private static async Task<Result> CreateStoreAsync(
        [FromRoute, ModelBinder<StronglyTypedIdBinder<StoreId>>] StoreId id,
        [FromBody] Request request,
        [FromServices] IRepository<Store> repository,
        [FromKeyedServices(nameof(Inventory))] IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
        => await repository.SingleOrDefaultAsResultAsync(new StoreByIdSpec(id), cancellationToken)
            .TapAsync(store => store.Update(request.Name, request.Description))
            .TapAsync(async _ => await unitOfWork.SaveChangesAsync(cancellationToken));
}
