using Common.Application.Auth;
using Common.Domain.ResultMonad;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Common.Application.Extensions;
using Common.Application.CQS;
using Products.Application.Stores.DTOs;
using Products.Application.Stores.Features.GetById;
using Products.Application.Stores.Features.GetStoreIdByOwnerId;
using Products.Domain.Stores;
using MediatR;

namespace Products.Endpoints.Stores.v1.My.Get;

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
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
        => await sender
                .Send(new GetStoreIdByOwnerIdQuery(currentUser.Id), cancellationToken)
                .BindAsync(storeId => sender.Send(new GetStoreByIdQuery(storeId), cancellationToken))
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
                    LastModifiedOn = storeDto.LastModifiedOn
                });
}
