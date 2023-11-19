using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using Common.Core.EndpointFilters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using NimbleMediator.Contracts;

namespace IdentityAndAuth.Features.Tokens.UseCases.Create;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapPost("", CreateAsync)
            .WithDescription("Create token by validating otp.")
            .Produces<Response>(StatusCodes.Status200OK)
            .AllowAnonymous()
            .TransformResultTo<Response>();
    }

    private static ValueTask<Result<Response>> CreateAsync(
        [FromBody] Request request,
        [FromServices] ISender mediator,
        CancellationToken cancellationToken)
        => mediator.SendAsync<Request, Result<Response>>(request, cancellationToken);
}
