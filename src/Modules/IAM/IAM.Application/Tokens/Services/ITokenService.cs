using Common.Domain.StronglyTypedIds;
using IAM.Domain.Identity.Sessions;

namespace IAM.Application.Tokens.Services;

public interface ITokenService
{
    (string accessToken, DateTimeOffset expiresAt) GenerateAccessToken(DateTimeOffset now, ApplicationUserId userId,
        SessionId sessionId, ICollection<string> roles);

    (byte[] refreshTokenBytes, DateTimeOffset expiresAt) GenerateRefreshToken(DateTimeOffset now);
}
