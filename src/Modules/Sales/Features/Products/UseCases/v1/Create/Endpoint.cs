using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Common.Core.Auth;
using Common.Core.Contracts.Results;
using Microsoft.AspNetCore.Mvc;
using Common.Core.Extensions;

namespace Sales.Features.Products.UseCases.v1.Create;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder salesApiGroup)
    {
        salesApiGroup
            .MapPost("{storeId}/products", CreateProductAsync)
            .WithDescription("Create a product.")
            .MustHavePermission(CustomActions.Create, CustomResources.Products)
            .Produces<Response>(StatusCodes.Status200OK)
            .TransformResultTo<Response>();
    }

    private static ValueTask<Result<Response>> CreateProductAsync(
        [FromRoute] Guid storeId,
        [FromBody] Request request,
        CancellationToken cancellationToken)
        => throw new NotImplementedException();
}
