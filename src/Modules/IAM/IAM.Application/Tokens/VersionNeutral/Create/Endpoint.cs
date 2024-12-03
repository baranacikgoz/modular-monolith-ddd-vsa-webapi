using Common.Domain.ResultMonad;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Common.Application.Extensions;
using IAM.Application.Identity.Services;
using IAM.Application.Tokens.Services;

namespace IAM.Application.Tokens.VersionNeutral.Create;

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

    private static async Task<Result<Response>> CreateAsync(
        [FromBody] Request request,
        [FromServices] IPhoneVerificationTokenService phoneVerificationTokenService,
        [FromServices] ITokenService tokenService,
        [FromServices] IUserService userService,
        CancellationToken cancellationToken)
        => await phoneVerificationTokenService
            .ValidateTokenAsync(request.PhoneNumber, request.PhoneVerificationToken, cancellationToken)
            .BindAsync(async () => await userService.GetByPhoneNumberAsync(request.PhoneNumber, cancellationToken))
            .BindAsync(async user => await tokenService.GenerateTokensAndUpdateUserAsync(user, cancellationToken))
            .MapAsync(tokenDto => new Response
            {
                AccessToken = tokenDto.AccessToken,
                AccessTokenExpiresAt = tokenDto.AccessTokenExpiresAt,
                RefreshToken = tokenDto.RefreshToken,
                RefreshTokenExpiresAt = tokenDto.RefreshTokenExpiresAt
            });
}
