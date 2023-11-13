using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using Common.Core.EndpointFilters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using NimbleMediator.Contracts;

namespace IdentityAndAuth.Features.Identity.UseCases.Users.ProvePhoneOwnership;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapPost("prove-phone-ownership", ProvePhoneOwnershipAsync)
            .WithDescription("Prove phone ownership by validating otp.")
            .Produces<Response>(StatusCodes.Status200OK)
            .AllowAnonymous()
            .AddEndpointFilter<ResultToResponseTransformer<Response>>();
    }
    private static ValueTask<Result<Response>> ProvePhoneOwnershipAsync(
        [FromBody] Request request,
        [FromServices] ISender mediator,
        CancellationToken cancellationToken)
        => mediator.SendAsync<Request, Result<Response>>(request, cancellationToken);
}
