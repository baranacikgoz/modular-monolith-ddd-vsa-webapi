using Common.Core.Auth;
using Common.Core.Contracts.Identity;
using Common.Core.Contracts.Results;
using Common.Core.Extensions;
using IdentityAndAuth.Features.Identity.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace IdentityAndAuth.Features.Identity.UseCases.Users.Current.Get;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapGet("", GetAsync)
            .WithDescription("Get current user.")
            .Produces<Response>(StatusCodes.Status200OK)
            .MustHavePermission(CustomActions.ReadMy, CustomResources.Users)
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> GetAsync(
        [FromServices] ICurrentUser currentUser,
        [FromServices] UserManager<ApplicationUser> userManager,
        CancellationToken cancellationToken)
        => await Result<Dto>
            .CreateAsync(taskToAwaitValue: async () => await userManager
                                                    .Users
                                                    .Where(x => x.Id == currentUser.Id)
                                                    .Select(x => new Dto(x.Id, x.Name, x.LastName, x.PhoneNumber!))
                                                    .SingleOrDefaultAsync(cancellationToken),
                    errorIfValueNull: Error.NotFound(nameof(ApplicationUser), currentUser.Id))
            .MapAsync(dto => new Response(
                                    dto.Id,
                                    dto.Name,
                                    dto.LastName,
                                    dto.PhoneNumber));

    private sealed record Dto(
        ApplicationUserId Id,
        string Name,
        string LastName,
        string PhoneNumber);
}
