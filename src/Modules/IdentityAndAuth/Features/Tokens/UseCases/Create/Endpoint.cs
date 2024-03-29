using Common.Core.Contracts.Results;
using Common.Core.Extensions;
using IdentityAndAuth.Features.Identity.Domain;
using IdentityAndAuth.Features.Tokens.Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace IdentityAndAuth.Features.Tokens.UseCases.Create;

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
            .BindAsync(async user => await tokenService.GenerateTokensAndUpdateUserAsync(user))
            .MapAsync(tokenDto => new Response(
                                    tokenDto.AccessToken,
                                    tokenDto.AccessTokenExpiresAt,
                                    tokenDto.RefreshToken,
                                    tokenDto.RefreshTokenExpiresAt));
}
