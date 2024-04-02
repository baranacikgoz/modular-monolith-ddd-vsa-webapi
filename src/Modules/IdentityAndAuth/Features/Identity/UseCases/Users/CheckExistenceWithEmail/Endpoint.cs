using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Common.Core.Extensions;
using Common.Core.Contracts.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using IdentityAndAuth.Features.Identity.Domain;
using Microsoft.EntityFrameworkCore;

namespace IdentityAndAuth.Features.Identity.UseCases.Users.CheckExistenceWithEmail;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapPost("check-existence-with-email", CheckExistenceWithEmail)
            .WithDescription("Check existence of user with email.")
            .AllowAnonymous()
            .Produces<Response>(StatusCodes.Status200OK)
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> CheckExistenceWithEmail(
        [FromBody] Request request,
        [FromServices] UserManager<ApplicationUser> userManager,
        CancellationToken cancellationToken)
        => await Result<bool>
            .CreateAsync(async () => await IsUserExistAsync(userManager, request.Email, cancellationToken))
            .MapAsync(userExists => new Response(userExists));

    private static Task<bool> IsUserExistAsync(
        UserManager<ApplicationUser> userManager,
        string email,
        CancellationToken cancellationToken)
        => userManager
            .Users
            .AnyAsync(x => x.Email == email, cancellationToken);
}
