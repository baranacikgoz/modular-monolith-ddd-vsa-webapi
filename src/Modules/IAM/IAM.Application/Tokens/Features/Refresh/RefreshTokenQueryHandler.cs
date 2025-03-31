using Common.Domain.ResultMonad;
using IAM.Application.Tokens.Services;
using Common.Application.CQS;
using IAM.Application.Tokens.DTOs;
using IAM.Domain.Errors;
using IAM.Application.Persistence;
using Microsoft.EntityFrameworkCore;
using Common.Application.Persistence;

namespace IAM.Application.Tokens.Features.Refresh;

public sealed class RefreshTokenQueryHandler(
    TimeProvider timeProvider,
    IIAMDbContext dbContext,
    ITokenService tokenService
    ) : IQueryHandler<RefreshTokenQuery, AccessTokenDto>
{
    public async Task<Result<AccessTokenDto>> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
    {
        var providedRefreshTokenHash = Convert.FromBase64String(request.RefreshToken);

        var userResult = await dbContext
            .Users
            .AsNoTracking()
            .TagWith(nameof(RefreshTokenQueryHandler))
            .Where(x => x.RefreshTokenHash == providedRefreshTokenHash)
            .Select(u => new
            {
                u.Id,
                u.RefreshTokenExpiresAt,
                Roles = dbContext
                    .UserRoles
                    .Where(ur => ur.UserId == u.Id)
                    .Join(dbContext.Roles,
                          ur => ur.RoleId,
                          r => r.Id,
                          (ur, r) => r.Name)
                    .Where(name => name != null)
                    .Select(name => name!)
                    .ToList()
            })
            .SingleAsResultAsync(cancellationToken);

        if (userResult.IsFailure)
        {
            return Result<AccessTokenDto>.Failure(userResult.Error!);
        }
        var user = userResult.Value!;

        if (user.RefreshTokenExpiresAt < timeProvider.GetUtcNow())
        {
            return TokenErrors.RefreshTokenExpired;
        }

        var utcNow = timeProvider.GetUtcNow();

        var (accessToken, accessTokenExpiresAt) = tokenService.GenerateAccessToken(utcNow, user.Id, user.Roles);

        return new AccessTokenDto
        {
            AccessToken = accessToken,
            AccessTokenExpiresAt = accessTokenExpiresAt
        };
    }
}
