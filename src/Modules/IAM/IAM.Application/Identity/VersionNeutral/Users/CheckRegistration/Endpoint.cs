using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Common.Domain.ResultMonad;
using Common.Application.Extensions;
using IAM.Domain.Identity;

namespace IAM.Application.Identity.VersionNeutral.Users.CheckRegistration;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapPost("check-registration", IsRegisteredAsync)
            .WithDescription("Check if a user is registered by given phone number.")
            .AllowAnonymous()
            .Produces<Response>(StatusCodes.Status200OK)
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> IsRegisteredAsync(
        [FromBody] Request request,
        [FromServices] UserManager<ApplicationUser> userManager,
        CancellationToken cancellationToken)
        => await Result<bool>
            .CreateAsync(async () => await IsUserExistAsync(userManager, request.PhoneNumber, cancellationToken))
            .MapAsync(userExists => new Response { IsRegistered = userExists });

    private static Task<bool> IsUserExistAsync(
        UserManager<ApplicationUser> userManager,
        string phoneNumber,
        CancellationToken cancellationToken)
        => userManager
            .Users
            .AnyAsync(x => x.PhoneNumber == phoneNumber, cancellationToken);
}
