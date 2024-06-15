using System.Security.Claims;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using Common.Infrastructure.Options;
using IAM.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace IAM.Application.Tokens.Services;

public interface ITokenService
{
    Task<Result<TokenDto>> GenerateTokensAndUpdateUserAsync(ApplicationUser user, CancellationToken cancellationToken);
    Task<Result<ClaimsPrincipal>> GetClaimsPrincipalByExpiredToken(string refreshToken, CancellationToken cancellationToken);
}
