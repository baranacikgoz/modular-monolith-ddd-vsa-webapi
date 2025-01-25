using Common.Domain.ResultMonad;
using IAM.Application.Tokens.DTOs;
using Common.Application.CQS;
using Common.Application.Persistence;
using IAM.Domain.Identity;
using IAM.Application.Users.Specifications;
using System.Security.Cryptography;
using Microsoft.Extensions.DependencyInjection;
using IAM.Application.Tokens.Services;
using Microsoft.AspNetCore.Identity;

namespace IAM.Application.Tokens.Features.Create;

public sealed class CreateTokensCommandHandler(
    IRepository<ApplicationUser> repository,
    UserManager<ApplicationUser> userManager,
    [FromKeyedServices(nameof(IAM))] IUnitOfWork unitOfWork,
    TimeProvider timeProvider,
    ITokenService tokenService
    ) : ICommandHandler<CreateTokensCommand, TokensDto>
{
    public async Task<Result<TokensDto>> Handle(CreateTokensCommand request, CancellationToken cancellationToken)
    {
        var userResult = await repository.SingleOrDefaultAsResultAsync(new UserByPhoneNumberSpec(request.PhoneNumber), cancellationToken);

        if (userResult.IsFailure)
        {
            return Result<TokensDto>.Failure(userResult.Error!);
        }
        var user = userResult.Value!;
        var roles = await userManager.GetRolesAsync(user);

        var utcNow = timeProvider.GetUtcNow();

        var (accessToken, accessTokenExpiresAt) = tokenService.GenerateAccessToken(utcNow, user.Id, roles);
        var (refreshTokenBytes, refreshTokenExpiresAt) = tokenService.GenerateRefreshToken(utcNow);

        user.UpdateRefreshToken(SHA256.HashData(refreshTokenBytes), refreshTokenExpiresAt);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new TokensDto
        {
            AccessToken = accessToken,
            AccessTokenExpiresAt = accessTokenExpiresAt,
            RefreshToken = Convert.ToBase64String(refreshTokenBytes),
            RefreshTokenExpiresAt = refreshTokenExpiresAt
        };
    }
}
