using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

/// <summary>
///     Connection to the Keycloak realm that owns every user, role and permission.
///     Two confidential clients are used: <see cref="ResourceClientId" /> (resource server, password direct
///     grant, service account for the Admin REST API, Authorization Services) and
///     <see cref="TrustedLoginClientId" /> (direct grant flow without a credential step, called only after the API
///     verified a one-time code itself).
/// </summary>
public class KeycloakOptions
{
    /// <summary>Base URL of the Keycloak server, e.g. <c>http://mm.keycloak:8080</c>. Must match Keycloak's hostname so token issuers validate.</summary>
    public required string BaseUrl { get; set; }

    public required string Realm { get; set; }

    /// <summary>Client id of the resource server. Also the expected <c>aud</c> of every access token.</summary>
    public required string ResourceClientId { get; set; }

    public required string ResourceClientSecret { get; set; }

    public required string TrustedLoginClientId { get; set; }

    public required string TrustedLoginClientSecret { get; set; }

    /// <summary>False only for local HTTP development; JwtBearer refuses plain-HTTP discovery otherwise.</summary>
    public required bool RequireHttpsMetadata { get; set; }

    /// <summary>
    ///     Upper bound for caching a positive or negative authorization decision. The effective TTL is the smaller of
    ///     this and the access token's remaining lifetime, so a decision can never outlive its token.
    /// </summary>
    public required int DecisionCacheMaxDurationSeconds { get; set; }

    /// <summary>Renew the service-account token this many seconds before it expires.</summary>
    public required int ServiceAccountTokenRefreshSkewSeconds { get; set; }

    public required int AttemptTimeoutSeconds { get; set; }

    public required int TotalRequestTimeoutSeconds { get; set; }

    /// <summary>Max concurrent Keycloak Admin <c>GET /users/{id}</c> calls one <c>GetUsersRequest</c> fans out for its cache misses.</summary>
    public required int UserLookupConcurrency { get; set; }

    /// <summary>L1 (in-process) lifetime of one user's cached display data.</summary>
    public required int UserDisplayCacheDurationSeconds { get; set; }

    /// <summary>L2 (Redis) lifetime of one user's cached display data. Long on purpose: a name changes only from the Keycloak console.</summary>
    public required int UserDisplayCacheDistributedDurationSeconds { get; set; }

    /// <summary>Lifetime of the negative entry cached for an id Keycloak does not know, so repeated lists do not re-ask for a deleted actor.</summary>
    public required int UserDisplayNegativeCacheDurationSeconds { get; set; }

    /// <summary>
    ///     Lifetime of the cached realm role graph (each composite realm role and the realm roles it includes) that
    ///     answers "which roles include this one". Composite roles change only from the realm file or the Keycloak console, so
    ///     a short window turns the one-call-per-composite read into one read per window instead of one per request. The same
    ///     window keeps the deduplicated members of a role that has indirect holders, so paging through them does not re-read Keycloak
    ///     for every page.
    /// </summary>
    public required int RoleGraphCacheSeconds { get; set; }

    public string Authority => $"{BaseUrl.TrimEnd('/')}/realms/{Realm}";
}

public class KeycloakOptionsValidator : CustomValidator<KeycloakOptions>
{
    public KeycloakOptionsValidator()
    {
        RuleFor(o => o.BaseUrl)
            .NotEmpty()
            .WithMessage("BaseUrl must not be empty.")
            .Must(url => Uri.TryCreate(url, UriKind.Absolute, out var uri) &&
                         (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
            .WithMessage("BaseUrl must be an absolute http(s) URL.");

        RuleFor(o => o.Realm)
            .NotEmpty()
            .WithMessage("Realm must not be empty.");

        RuleFor(o => o.ResourceClientId)
            .NotEmpty()
            .WithMessage("ResourceClientId must not be empty.");

        RuleFor(o => o.ResourceClientSecret)
            .NotEmpty()
            .WithMessage("ResourceClientSecret must not be empty.")
            .Must((_, secret, context) => context.IsDevelopment() || !secret.Contains("change-me", StringComparison.Ordinal))
            .WithMessage(
                $"{nameof(KeycloakOptions)}.{nameof(KeycloakOptions.ResourceClientSecret)} still holds the realm file's " +
                "development value outside Development. Rotate it in Keycloak and set KeycloakOptions__ResourceClientSecret.");

        RuleFor(o => o.TrustedLoginClientId)
            .NotEmpty()
            .WithMessage("TrustedLoginClientId must not be empty.")
            .NotEqual(o => o.ResourceClientId)
            .WithMessage("TrustedLoginClientId must differ from ResourceClientId: the two clients bind different direct grant flows.");

        RuleFor(o => o.TrustedLoginClientSecret)
            .NotEmpty()
            .WithMessage("TrustedLoginClientSecret must not be empty.")
            .Must((_, secret, context) => context.IsDevelopment() || !secret.Contains("change-me", StringComparison.Ordinal))
            .WithMessage(
                $"{nameof(KeycloakOptions)}.{nameof(KeycloakOptions.TrustedLoginClientSecret)} still holds the realm file's " +
                "development value outside Development: this secret is equivalent to every user's password. Rotate it in " +
                "Keycloak and set KeycloakOptions__TrustedLoginClientSecret.");

        RuleFor(o => o.DecisionCacheMaxDurationSeconds)
            .GreaterThan(0)
            .WithMessage("DecisionCacheMaxDurationSeconds must be greater than 0.");

        RuleFor(o => o.ServiceAccountTokenRefreshSkewSeconds)
            .GreaterThanOrEqualTo(0)
            .WithMessage("ServiceAccountTokenRefreshSkewSeconds must not be negative.");

        RuleFor(o => o.AttemptTimeoutSeconds)
            .GreaterThan(0)
            .WithMessage("AttemptTimeoutSeconds must be greater than 0.");

        RuleFor(o => o.TotalRequestTimeoutSeconds)
            .GreaterThanOrEqualTo(o => o.AttemptTimeoutSeconds)
            .WithMessage("TotalRequestTimeoutSeconds must be at least AttemptTimeoutSeconds.");

        RuleFor(o => o.UserLookupConcurrency)
            .GreaterThan(0)
            .WithMessage("UserLookupConcurrency must be greater than 0.");

        RuleFor(o => o.UserDisplayCacheDurationSeconds)
            .GreaterThan(0)
            .WithMessage("UserDisplayCacheDurationSeconds must be greater than 0.");

        RuleFor(o => o.UserDisplayCacheDistributedDurationSeconds)
            .GreaterThanOrEqualTo(o => o.UserDisplayCacheDurationSeconds)
            .WithMessage("UserDisplayCacheDistributedDurationSeconds must be at least UserDisplayCacheDurationSeconds.");

        RuleFor(o => o.UserDisplayNegativeCacheDurationSeconds)
            .GreaterThan(0)
            .WithMessage("UserDisplayNegativeCacheDurationSeconds must be greater than 0.");

        RuleFor(o => o.RoleGraphCacheSeconds)
            .GreaterThan(0)
            .WithMessage("RoleGraphCacheSeconds must be greater than 0.");
    }
}
