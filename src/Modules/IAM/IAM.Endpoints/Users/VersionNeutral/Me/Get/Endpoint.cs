using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using IAM.Application.Persistence;
using IAM.Domain.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace IAM.Endpoints.Users.VersionNeutral.Me.Get;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapGet("me", GetMeAsync)
            .WithDescription("Get current user.")
            .Produces<Response>()
            .MustHavePermission(CustomActions.ReadMy, CustomResources.ApplicationUsers)
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> GetMeAsync(
        ICurrentUser currentUser,
        IIAMDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext
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
                CreatedOn = u.CreatedOn,
                LastModifiedBy = u.Id,
                LastModifiedOn = u.LastModifiedOn
            })
            .SingleAsResultAsync(nameof(ApplicationUser), cancellationToken);
    }
}
