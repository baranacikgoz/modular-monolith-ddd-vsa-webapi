using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.IAM;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

// Split-deployment PoC probe — see docs/split-deployment-poc.md for full context.
// This endpoint exists only to make the cross-process MassTransit round-trip observable via a single curl.
// It intentionally lives in the Products module to prove that Products can call IAM without IAM being
// co-located in the same process. Remove if the PoC is no longer needed.
namespace Products.Endpoints.Probe.v1;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder group)
    {
        group.MapGet("cross-module", HandleAsync)
             .WithDescription("PoC: calls IAM module cross-process via MassTransit and returns user IDs.")
             .AllowAnonymous();
    }

    private static async Task<IResult> HandleAsync(
        [AsParameters] Request request,
        [FromServices] IInterModuleRequestClient<GetSeedUserIdsRequest, GetSeedUserIdsResponse> client,
        CancellationToken cancellationToken)
    {
        var response = await client.SendAsync(new GetSeedUserIdsRequest(request.Count), cancellationToken);
        return Results.Ok(response);
    }
}
