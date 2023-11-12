using Common.Core.Contracts.Results;
using IdentityAndAuth.Features.Identity.Domain;

namespace IdentityAndAuth.Features.Tokens.Domain.Services;

internal interface ITokenService
{
    Task<Result<TokenDto>> GenerateTokensAndUpdateUserAsync(ApplicationUser user);
    Task<Result> ValidateRefreshTokenAsync(string refreshToken, string phoneNumber);
}
