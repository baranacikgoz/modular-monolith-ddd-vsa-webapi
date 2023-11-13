using Common.Core.Auth;
using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using Common.Core.EndpointFilters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using NimbleMediator.Contracts;

namespace IdentityAndAuth.Features.Identity.UseCases.Users.Get;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapGet("{id}", GetAsync)
            .WithDescription("Get a user by id.")
            .Produces<Response>(StatusCodes.Status200OK)
            .MustHavePermission(RfActions.Read, RfResources.Users)
            .AddEndpointFilter<ResultToMinimalApiResponseFilter<Response>>();
    }
    private static ValueTask<Result<Response>> GetAsync(
        [FromRoute] Guid id,
        [FromServices] ISender mediator,
        CancellationToken cancellationToken)
        => mediator.SendAsync<Request, Result<Response>>(new(id), cancellationToken);
}
