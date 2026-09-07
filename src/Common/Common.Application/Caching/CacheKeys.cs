using System.Security.Cryptography;
using System.Text;
using Common.Domain.StronglyTypedIds;

namespace Common.Application.Caching;

public static class CacheKeys
{
    public static class For
    {
        public static string Otp(string phoneNumber, string purpose, string? contextId = null)
        {
            var input = string.IsNullOrEmpty(contextId) ? phoneNumber : $"{phoneNumber}|{contextId}";
            var hash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(input)));
            return $"otp:{purpose}:{hash}";
        }

        /// <summary>
        ///     Cached Keycloak authorization decision for one access token (<paramref name="jti" />) and one
        ///     <c>resource#scope</c> permission. Keyed by jti, not sid, so the entry can never outlive the token.
        /// </summary>
        public static string AuthorizationDecision(string jti, string permission)
        {
            return $"authz_decision:{jti}:{permission}";
        }

        /// <summary>
        ///     FusionCache tag on every decision made under one Keycloak session (<c>sid</c>), so revoking that
        ///     session purges its decisions without knowing the token jtis.
        /// </summary>
        public static string AuthorizationDecisionSessionTag(string sessionId)
        {
            return $"authz_session:{sessionId}";
        }

        /// <summary>FusionCache tag on every decision made for one user; sign-out-everywhere purges by it.</summary>
        public static string AuthorizationDecisionUserTag(ApplicationUserId userId)
        {
            return $"authz_user:{userId}";
        }
    }
}
