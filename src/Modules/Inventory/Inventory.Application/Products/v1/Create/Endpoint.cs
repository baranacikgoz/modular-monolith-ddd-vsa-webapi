using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Common.Domain.ResultMonad;
using Common.Application.Auth;
using Common.Application.Extensions;

namespace Inventory.Application.Products.v1.Create;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder salesApiGroup)
    {
        salesApiGroup
            .MapPost("", CreateProductAsync)
            .WithDescription("Create a product.")
            .MustHavePermission(CustomActions.Create, CustomResources.Products)
            .Produces<Response>(StatusCodes.Status200OK)
            .TransformResultTo<Response>();
    }

    private static ValueTask<Result<Response>> CreateProductAsync(
        [FromBody] Request request,
        CancellationToken cancellationToken)
        => throw new NotImplementedException();
}
