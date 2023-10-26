using Common.Core.Contracts.Results;
using IdentityAndAuth.Features.Users.Domain;
using IdentityAndAuth.Identity;

namespace IdentityAndAuth.Features.Tokens.Services;

public interface ITokenService
{
    Task<Result<TokenDto>> GenerateTokensAndUpdateUserAsync(ApplicationUser user);
    Task<Result> ValidateRefreshTokenAsync(string refreshToken, string phoneNumber);
}
