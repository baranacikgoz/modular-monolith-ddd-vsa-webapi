using Common.Core.Contracts.Results;
using IdentityAndAuth.Features.Users.Domain;

namespace IdentityAndAuth.Features.Tokens.Services;

internal interface ITokenService
{
    Task<Result<TokenDto>> GenerateTokensAndUpdateUserAsync(ApplicationUser user);
    Task<Result> ValidateRefreshTokenAsync(string refreshToken, string phoneNumber);
}
