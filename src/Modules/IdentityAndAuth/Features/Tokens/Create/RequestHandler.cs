using Common.Core.Contracts.Results;
using NimbleMediator.Contracts;
using IdentityAndAuth.Features.Tokens.Services;
using IdentityAndAuth.Features.Users.Services;
using IdentityAndAuth.Features.Users.Services.PhoneVerificationToken;

namespace IdentityAndAuth.Features.Tokens.Create;

internal sealed class RequestHandler(
        IPhoneVerificationTokenService phoneVerificationTokenService,
        ITokenService tokenService,
        IUserService userService
        ) : IRequestHandler<Request, Result<Response>>
{
    public async ValueTask<Result<Response>> HandleAsync(Request request, CancellationToken cancellationToken)
    {
        return await phoneVerificationTokenService
            .ValidateTokenAsync(request.PhoneNumber, request.PhoneVerificationToken, cancellationToken)
            .BindAsync(() => userService.GetByPhoneNumberAsync(request.PhoneNumber))
            .BindAsync(tokenService.GenerateTokensAndUpdateUserAsync)
            .MapAsync(tokenDto => new Response(
                tokenDto.AccessToken,
                tokenDto.AccessTokenExpiresAt,
                tokenDto.RefreshToken,
                tokenDto.RefreshTokenExpiresAt));
    }
}
