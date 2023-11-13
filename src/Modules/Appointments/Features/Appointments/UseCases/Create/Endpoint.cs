using Common.Core.Auth;
using Common.Core.Contracts.Results;
using Common.Core.EndpointFilters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NimbleMediator.Contracts;

namespace Appointments.Features.Appointments.UseCases.Create;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder venuesApiGroup)
    {
        venuesApiGroup
            .MapPost("", CreateAsync)
            .WithDescription("Create an appointment.")
            .MustHavePermission(RfActions.Create, RfResources.Appointments)
            .Produces<Response>(StatusCodes.Status200OK)
            .AddEndpointFilter<ResultToResponseTransformer<Response>>();
    }
    private static ValueTask<Result<Response>> CreateAsync(
        [FromBody] Request request,
        [FromServices] ISender mediator,
        CancellationToken cancellationToken)
        => mediator.SendAsync<Request, Result<Response>>(request, cancellationToken);
}
