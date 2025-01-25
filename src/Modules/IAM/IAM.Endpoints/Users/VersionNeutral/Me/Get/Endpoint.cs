using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using IAM.Application.Users.Features.GetById;
using MediatR;
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
            .MapGet("me", GetAsync)
            .WithDescription("Get current user.")
            .Produces<Response>(StatusCodes.Status200OK)
            .MustHavePermission(CustomActions.ReadMy, CustomResources.ApplicationUsers)
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> GetAsync(
        [FromServices] ICurrentUser currentUser,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
        => await sender
                .Send(new GetUserByIdQuery(currentUser.Id), cancellationToken)
                .MapAsync(user => new Response
                {
                    Id = user.Id,
                    Name = user.Name,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    BirthDate = user.BirthDate,
                    CreatedBy = user.CreatedBy,
                    CreatedOn = user.CreatedOn,
                    LastModifiedBy = user.LastModifiedBy,
                    LastModifiedOn = user.LastModifiedOn
                });
}
