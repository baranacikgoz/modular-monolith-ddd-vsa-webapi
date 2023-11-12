using Common.Core.Contracts.Results;
using IdentityAndAuth.Features.Identity.Domain;
using IdentityAndAuth.Features.Tokens.Domain.Services;
using NimbleMediator.Contracts;

namespace IdentityAndAuth.Features.Tokens.UseCases.Create;

internal sealed class RequestHandler(
    IPhoneVerificationTokenService phoneVerificationTokenService,
    ITokenService tokenService,
    IUserService userService
    ) : IRequestHandler<Request, Result<Response>>
{
    public async ValueTask<Result<Response>> HandleAsync(Request request, CancellationToken cancellationToken)
        => await phoneVerificationTokenService
            .ValidateTokenAsync(request.PhoneNumber, request.PhoneVerificationToken, cancellationToken)
            .BindAsync(userService.GetByPhoneNumberAsync(request.PhoneNumber, cancellationToken))
            .BindAsync(tokenService.GenerateTokensAndUpdateUserAsync)
            .MapAsync(tokenDto => new Response(
                tokenDto.AccessToken,
                tokenDto.AccessTokenExpiresAt,
                tokenDto.RefreshToken,
                tokenDto.RefreshTokenExpiresAt));
}
