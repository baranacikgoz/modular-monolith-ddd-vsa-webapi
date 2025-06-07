using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using IAM.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace IAM.Endpoints.Users.VersionNeutral.Get;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapGet("{id}", GetAsync)
            .WithDescription("Get a user by id.")
            .MustHavePermission(CustomActions.Read, CustomResources.ApplicationUsers)
            .Produces<Response>(StatusCodes.Status200OK)
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> GetAsync(
        [AsParameters] Request request,
        [FromServices] IAMDbContext dbContext,
        CancellationToken cancellationToken)
        => await dbContext
            .Users
            .TagWith(nameof(GetAsync), request.Id)
            .Where(u => u.Id == request.Id)
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
            .SingleAsResultAsync(cancellationToken);
}
