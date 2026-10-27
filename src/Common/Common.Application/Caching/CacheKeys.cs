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

        public static string OtpResend(string phoneNumber, string purpose, string? contextId = null)
        {
            var input = string.IsNullOrEmpty(contextId) ? $"{phoneNumber}|{purpose}" : $"{phoneNumber}|{purpose}|{contextId}";
            var hash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(input)));
            return $"otp:resend:{hash}";
        }

        // Deliberately phone-only, no purpose, no contextId. OtpResend above is keyed per-context so a
        // caller can vary ContextId (or IP, for the rate limiter) to bypass it entirely; this is the
        // backstop that still caps total SMS volume to one phone number regardless of context.
        public static string OtpPhoneQuota(string phoneNumber)
        {
            var hash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(phoneNumber)));
            return $"otp:phonequota:{hash}";
        }

        /// <summary>
        ///     Same backstop as <see cref="OtpPhoneQuota" />, for the email channel. A separate bucket from the
        ///     phone quota so SMS and email spend never cannibalize each other's cap. Callers must pass an
        ///     already-normalized (trimmed, lowercased) address, or the same mailbox gets one bucket per casing.
        /// </summary>
        public static string OtpEmailQuota(string normalizedEmail)
        {
            var hash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(normalizedEmail)));
            return $"otp:emailquota:{hash}";
        }

        /// <summary>
        ///     The full set of <c>resource#scope</c> permissions Keycloak granted one access token
        ///     (<paramref name="jti" />), fetched once per token and answered locally for every later decision.
        ///     Keyed by jti, not sid, so the entry can never outlive the token.
        /// </summary>
        public static string AuthorizationPermissions(string jti)
        {
            return $"authz_permissions:{jti}";
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

        /// <summary>
        ///     Cached Keycloak display data (name, email) of one user for the bulk <c>GetUsersRequest</c> lookup.
        ///     Evicted by <c>DeleteUserAsync</c>; a name edit in the Keycloak console surfaces when the entry expires.
        /// </summary>
        public static string UserDisplay(ApplicationUserId userId)
        {
            return $"user_display:{userId}";
        }

        /// <summary>
        ///     The realm role graph (every composite realm role and the realm roles it includes) that answers "which roles
        ///     include this one". One entry for the configured realm; it expires after <c>KeycloakOptions.RoleGraphCacheSeconds</c> and can be
        ///     evicted by removing this key.
        /// </summary>
        public const string KeycloakRoleGraph = "keycloak_role_graph";
    }
}
