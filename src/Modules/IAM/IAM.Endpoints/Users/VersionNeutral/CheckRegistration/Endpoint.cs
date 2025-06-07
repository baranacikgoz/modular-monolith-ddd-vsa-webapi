using Common.Application.Extensions;
using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using IAM.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace IAM.Endpoints.Users.VersionNeutral.CheckRegistration;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapGet("check-registration", IsRegisteredAsync)
            .WithDescription("Check if a user is registered by given phone number.")
            .AllowAnonymous()
            .Produces<Response>(StatusCodes.Status200OK)
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> IsRegisteredAsync(
        [AsParameters] Request request,
        [FromServices] IAMDbContext dbContext,
        CancellationToken cancellationToken)
        => await dbContext
            .Users
            .TagWith(nameof(IsRegisteredAsync))
            .Where(u => u.PhoneNumber == request.PhoneNumber)
            .AnyAsResultAsync(cancellationToken)
            .MapAsync(any => new Response { IsRegistered = any });
}
