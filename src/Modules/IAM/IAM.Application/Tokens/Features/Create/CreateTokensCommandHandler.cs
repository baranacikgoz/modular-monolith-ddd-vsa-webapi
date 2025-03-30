using Common.Domain.ResultMonad;
using IAM.Application.Tokens.DTOs;
using Common.Application.CQS;
using System.Security.Cryptography;
using IAM.Application.Tokens.Services;
using IAM.Application.Persistence;
using Common.Application.Persistence;

namespace IAM.Application.Tokens.Features.Create;

public sealed class CreateTokensCommandHandler(
    IAMDbContext dbContext,
    TimeProvider timeProvider,
    ITokenService tokenService
    ) : ICommandHandler<CreateTokensCommand, TokensDto>
{
    public async Task<Result<TokensDto>> Handle(CreateTokensCommand request, CancellationToken cancellationToken)
    {
        var userResult = await dbContext
            .Users
            .TagWith(nameof(CreateTokensCommandHandler), request.PhoneNumber)
            .Where(x => x.PhoneNumber == request.PhoneNumber)
            .Select(u => new
            {
                User = u,
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
            return Result<TokensDto>.Failure(userResult.Error!);
        }
        var userObj = userResult.Value!;
        var user = userObj.User;

        var utcNow = timeProvider.GetUtcNow();

        var (accessToken, accessTokenExpiresAt) = tokenService.GenerateAccessToken(utcNow, user.Id, userObj.Roles);
        var (refreshTokenBytes, refreshTokenExpiresAt) = tokenService.GenerateRefreshToken(utcNow);

        user.UpdateRefreshToken(SHA256.HashData(refreshTokenBytes), refreshTokenExpiresAt);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new TokensDto
        {
            AccessToken = accessToken,
            AccessTokenExpiresAt = accessTokenExpiresAt,
            RefreshToken = Convert.ToBase64String(refreshTokenBytes),
            RefreshTokenExpiresAt = refreshTokenExpiresAt
        };
    }
}
