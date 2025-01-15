using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using Common.Application.Auth;
using Common.Application.DTOs;
using Common.Application.Extensions;
using Common.Application.ModelBinders;
using Common.Application.Pagination;
using Common.Application.Persistence;
using Common.Domain.Entities;
using Common.Domain.ResultMonad;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Products.Domain.Stores;

namespace Products.Application.Stores.v1.History;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder storeProductsApiGroup)
    {
        storeProductsApiGroup
            .MapPost("{id}/history", GetStoreHistoryAsync)
            .WithDescription("Get Store's history.")
            .MustHavePermission(CustomActions.Read, CustomResources.Stores)
            .AllowAnonymous()
            .Produces<PaginationResult<EventDto>>(StatusCodes.Status200OK);
    }

    private static async Task<PaginationResult<EventDto>> GetStoreHistoryAsync(
        [FromRoute, ModelBinder<StronglyTypedIdBinder<StoreId>>] StoreId id,
        [FromBody] Request request,
        [FromServices] IRepository<Store> repository,
        CancellationToken cancellationToken)
        => await repository.GetEventHistoryAsync(id, request, cancellationToken);
}
