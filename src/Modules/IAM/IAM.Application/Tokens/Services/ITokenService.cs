using Common.Domain.StronglyTypedIds;

namespace IAM.Application.Tokens.Services;

public interface ITokenService
{
    (string accessToken, DateTimeOffset expiresAt) GenerateAccessToken(DateTimeOffset now, ApplicationUserId userId, ICollection<string> roles);
    (byte[] refreshTokenBytes, DateTimeOffset expiresAt) GenerateRefreshToken(DateTimeOffset now);
}
