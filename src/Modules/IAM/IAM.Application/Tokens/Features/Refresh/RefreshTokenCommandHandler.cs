using Common.Domain.ResultMonad;
using IAM.Application.Tokens.Services;
using Common.Application.CQS;
using IAM.Application.Tokens.DTOs;
using Common.Application.Persistence;
using IAM.Application.Users.Specifications;
using Microsoft.AspNetCore.Identity;
using IAM.Domain.Identity;
using IAM.Domain.Errors;

namespace IAM.Application.Tokens.Features.Refresh;

public sealed class RefreshTokenCommandHandler(
    TimeProvider timeProvider,
    IRepository<ApplicationUser> repository,
    UserManager<ApplicationUser> userManager,
    ITokenService tokenService
    ) : ICommandHandler<RefreshTokenCommand, AccessTokenDto>
{
    public async Task<Result<AccessTokenDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var providedRefreshTokenHash = Convert.FromBase64String(request.RefreshToken);
        var userResult = await repository.SingleOrDefaultAsResultAsync(new UserByRefreshTokenHashSpec(providedRefreshTokenHash), cancellationToken);

        if (userResult.IsFailure)
        {
            return Result<AccessTokenDto>.Failure(userResult.Error!);
        }
        var user = userResult.Value!;

        if (user.RefreshTokenExpiresAt < timeProvider.GetUtcNow())
        {
            return TokenErrors.RefreshTokenExpired;
        }

        var roles = await userManager.GetRolesAsync(user);

        var utcNow = timeProvider.GetUtcNow();

        var (accessToken, accessTokenExpiresAt) = tokenService.GenerateAccessToken(utcNow, user.Id, roles);

        return new AccessTokenDto
        {
            AccessToken = accessToken,
            AccessTokenExpiresAt = accessTokenExpiresAt
        };
    }
}
