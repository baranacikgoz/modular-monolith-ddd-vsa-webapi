using System.Security.Claims;
using Common.Infrastructure.Options;
using IAM.Domain.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Domain.Extensions;
using Common.Domain.StronglyTypedIds;
using IAM.Application.Identity.Services;
using IAM.Application.Tokens.Services;

namespace IAM.Application.Tokens.VersionNeutral.Refresh;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapPost("refresh", RefreshAsync)
            .WithDescription("Refresh token by validating expired access token and refresh token.")
            .Produces<Response>(StatusCodes.Status200OK)
            .AllowAnonymous()
            .TransformResultTo<Response>();
    }
    private static async Task<Result<Response>> RefreshAsync(
        [FromBody] Request request,
        [FromServices] IUserService userService,
        [FromServices] ITokenService tokenService,
        CancellationToken cancellationToken)
        => await tokenService.GetClaimsPrincipalByExpiredToken(request.RefreshToken, cancellationToken)
            .BindAsync(async claimsPrincipal => await userService.GetByClaimsPrincipalAsync(claimsPrincipal, cancellationToken))
            .BindAsync(async user => await tokenService.GenerateTokensAndUpdateUserAsync(user, cancellationToken))
            .MapAsync(tokenDto => new Response(
                                        tokenDto.AccessToken,
                                        tokenDto.AccessTokenExpiresAt,
                                        tokenDto.RefreshToken,
                                        tokenDto.RefreshTokenExpiresAt));
}
