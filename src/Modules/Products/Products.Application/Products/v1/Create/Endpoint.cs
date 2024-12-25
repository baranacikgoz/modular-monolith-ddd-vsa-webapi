using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Common.Domain.ResultMonad;
using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Products.Domain.Products;

namespace Products.Application.Products.v1.Create;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder productsApiGroup)
    {
        productsApiGroup
            .MapPost("", CreateProductAsync)
            .WithDescription("Create a product.")
            //.MustHavePermission(CustomActions.Create, CustomResources.Products)
            .AllowAnonymous()
            .Produces<Response>(StatusCodes.Status200OK)
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> CreateProductAsync(
        [FromBody] Request request,
        [FromServices] IRepository<Product> repository,
        [FromKeyedServices(nameof(Products))] IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
        => await Result<Product>
            .Create(() => Product.Create(request.Name, request.Description))
            .Tap(product => repository.Add(product))
            .TapAsync(async _ => await unitOfWork.SaveChangesAsync(cancellationToken))
            .MapAsync(product => new Response { Id = product.Id });
}
