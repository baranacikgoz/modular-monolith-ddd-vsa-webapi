using Common.Application.Auth;
using Common.Domain.ResultMonad;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Common.Application.Extensions;
using Common.Application.ModelBinders;
using Products.Domain.Stores;
using Products.Application.Stores.Features.GetById;
using MediatR;

namespace Products.Endpoints.Stores.v1.Get;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder myStoresApiGroup)
    {
        myStoresApiGroup
            .MapGet("{id}", GetStoreAsync)
            .WithDescription("Get store.")
            //.MustHavePermission(CustomActions.Read, CustomResources.Stores)
            .AllowAnonymous()
            .Produces<Response>(StatusCodes.Status200OK)
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> GetStoreAsync(
        [FromRoute, ModelBinder<StronglyTypedIdBinder<StoreId>>] StoreId id,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
        => await sender
                .Send(new GetStoreByIdQuery(id), cancellationToken)
                .MapAsync(storeDto => new Response
                {
                    Id = storeDto.Id,
                    OwnerId = storeDto.OwnerId,
                    Name = storeDto.Name,
                    Description = storeDto.Description,
                    Address = storeDto.Address,
                    ProductCount = storeDto.ProductCount,
                    CreatedBy = storeDto.CreatedBy,
                    CreatedOn = storeDto.CreatedOn,
                    LastModifiedBy = storeDto.LastModifiedBy,
                    LastModifiedOn = storeDto.LastModifiedOn,
                });
}
