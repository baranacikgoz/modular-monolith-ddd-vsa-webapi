using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using IAM.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace IAM.Endpoints.Users.VersionNeutral.Me.Get;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapGet("me", GetMeAsync)
            .WithDescription("Get current user.")
            .Produces<Response>(StatusCodes.Status200OK)
            .MustHavePermission(CustomActions.ReadMy, CustomResources.ApplicationUsers)
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> GetMeAsync(
        [FromServices] ICurrentUser currentUser,
        [FromServices] IAMDbContext dbContext,
        CancellationToken cancellationToken)
        => await dbContext
            .Users
            .TagWith(nameof(GetMeAsync), currentUser.Id)
            .Where(u => u.Id == currentUser.Id)
            .Select(u => new Response
            {
                Id = u.Id,
                Name = u.Name,
                LastName = u.LastName,
                PhoneNumber = u.PhoneNumber!,
                BirthDate = u.BirthDate,
                CreatedBy = u.Id,
                CreatedOn = DateTime.UtcNow,
                LastModifiedBy = u.Id,
                LastModifiedOn = DateTime.UtcNow
            })
            .SingleAsResultAsync(cancellationToken);
}
