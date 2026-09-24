using System.Collections.Concurrent;
using Common.Application.Caching;
using Common.Application.Options;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Notifications;
using Common.Tests;
using IAM.Application.Captcha.Services;
using IAM.Application.Keycloak;
using IAM.Domain.Errors;
using IAM.Infrastructure.Captcha.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Testcontainers.Keycloak;
using ZiggyCreatures.Caching.Fusion;

namespace IAM.Tests;

/// <summary>
///     Boots a real Keycloak (realm imported from <c>keycloak/realm-modular-monolith.json</c>) next to Postgres.
///     Every IAM test goes through the real JwtBearer pipeline and real permission decisions; only OTP delivery
///     and captcha are replaced with in-process fakes.
/// </summary>
public class IntegrationTestWebAppFactory : IntegrationTestFactory
{
    public const string KeycloakImage = "quay.io/keycloak/keycloak:26.7";

    private readonly KeycloakContainer _keycloakContainer = new KeycloakBuilder(KeycloakImage)
        .WithRealm(TestPaths.RealmFile)
        .Build();

    public string KeycloakBaseAddress => _keycloakContainer.GetBaseAddress().TrimEnd('/');

    protected override string[] GetActiveModules()
    {
        // Notifications owns the device registry every login binds to; Outbox backs its DbContext.
        return ["IAM", "Notifications", "Outbox"];
    }

    protected override bool UseTestAuthentication => false;

    public override async ValueTask InitializeAsync()
    {
        await _keycloakContainer.StartAsync();
        await base.InitializeAsync();
    }

    public override async ValueTask DisposeAsync()
    {
        try
        {
            await base.DisposeAsync();
        }
        finally
        {
            await _keycloakContainer.DisposeAsync();
        }

        GC.SuppressFinalize(this);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        // Both channels: UseSetting for registration-time reads, in-memory config for runtime IOptions
        // (the JSON config files are added after host settings and would otherwise win).
        builder.UseSetting("KeycloakOptions:BaseUrl", KeycloakBaseAddress);

        builder.ConfigureAppConfiguration((_, config) =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string?>
            {
                { "KeycloakOptions:BaseUrl", KeycloakBaseAddress },
                { "FeatureManagement:IAM.Captcha", "true" },
                // This collection covers the PhoneNumber scheme (phone-OTP routes plus the scheme-independent
                // email-OTP routes), independent of which scheme a given fork actually deploys. The Email
                // scheme's exclusive routes are covered by EmailSchemeWebAppFactory, which boots serially.
                { "IdentitySchemeOptions:Scheme", "PhoneNumber" },
                // Production values (1/15s, 5/60s) are far too tight for a test class hitting the same
                // in-process, per-IP bucket several times back to back; every test client shares one IP
                // here, unlike real traffic. Relaxed to the same headroom the other dedicated-policy
                // *RateLimitingPolicyTests classes construct by hand for the same reason.
                { "CustomRateLimitingOptions:Email:Limit", "1000" },
                { "CustomRateLimitingOptions:Email:PeriodInMs", "1000" },
                { "CustomRateLimitingOptions:OtpVerify:Limit", "1000" },
                { "CustomRateLimitingOptions:OtpVerify:PeriodInMs", "1000" }
            });
        });

        builder.ConfigureTestServices(services =>
        {
            services.AddSingleton<ICaptchaService, DummyCaptchaService>();

            // Bypass MassTransit/RabbitMQ for OTP inter-module requests.
            // The fakes replicate Notifications' OtpService behavior in-process using the same
            // FusionCache + CacheKeys contract, so tests can pre-seed cache directly.
            services.AddSingleton<IInterModuleRequestClient<SendPhoneOtpRequest, SendPhoneOtpResponse>,
                InProcessSendOtpClient>();
            services.AddSingleton<IInterModuleRequestClient<VerifyPhoneOtpRequest, VerifyPhoneOtpResponse>,
                InProcessVerifyOtpClient>();
            services.AddSingleton<IInterModuleRequestClient<SendEmailOtpRequest, SendEmailOtpResponse>,
                InProcessSendEmailOtpClient>();
            services.AddSingleton<IInterModuleRequestClient<VerifyEmailOtpRequest, VerifyEmailOtpResponse>,
                InProcessVerifyEmailOtpClient>();
            services.AddSingleton<IInterModuleRequestClient<IssueVerificationTokenRequest, IssueVerificationTokenResponse>,
                InProcessIssueVerificationTokenClient>();

            services.Decorate<IKeycloakAdminClient, FaultInjectingKeycloakAdminClient>();
        });
    }
}

/// <summary>
///     Test-only seam for PR #149 #3 (self-registration rollback): lets a test mark a not-yet-created username
///     (phone number or email, whichever the scheme provisions) so its role assignment fails once, without
///     touching real Keycloak.
/// </summary>
internal sealed class FaultInjectingKeycloakAdminClient(IKeycloakAdminClient inner) : IKeycloakAdminClient
{
    private static readonly ConcurrentDictionary<string, byte> PhonesToFailRoleAssignmentFor = new();
    private static readonly ConcurrentDictionary<ApplicationUserId, byte> UserIdsPendingRoleAssignmentFailure = new();

    public static void FailNextRoleAssignmentFor(string username)
    {
        PhonesToFailRoleAssignmentFor[username] = 0;
    }

    public async Task<Result<ApplicationUserId>> CreateUserAsync(CreateKeycloakUser user, CancellationToken cancellationToken)
    {
        var result = await inner.CreateUserAsync(user, cancellationToken);
        if (result.Value is { } createdUserId && PhonesToFailRoleAssignmentFor.TryRemove(user.Username, out _))
        {
            UserIdsPendingRoleAssignmentFailure[createdUserId] = 0;
        }

        return result;
    }

    public Task<Result> AssignRealmRoleAsync(ApplicationUserId userId, string roleName, CancellationToken cancellationToken)
    {
        return UserIdsPendingRoleAssignmentFailure.TryRemove(userId, out _)
            ? Task.FromResult<Result>(IdentityErrors.IdentityProviderUnavailable)
            : inner.AssignRealmRoleAsync(userId, roleName, cancellationToken);
    }

    public Task DeleteUserAsync(ApplicationUserId userId, CancellationToken cancellationToken)
    {
        return inner.DeleteUserAsync(userId, cancellationToken);
    }

    public Task<Result<KeycloakUser>> GetUserAsync(ApplicationUserId userId, CancellationToken cancellationToken)
    {
        return inner.GetUserAsync(userId, cancellationToken);
    }

    public Task<KeycloakUser?> FindUserByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        return inner.FindUserByUsernameAsync(username, cancellationToken);
    }

    public Task<KeycloakUser?> FindUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return inner.FindUserByEmailAsync(email, cancellationToken);
    }

    public Task<KeycloakUserPage> SearchUsersAsync(string? searchTerm, int skip, int take, CancellationToken cancellationToken)
    {
        return inner.SearchUsersAsync(searchTerm, skip, take, cancellationToken);
    }

    public Task<IReadOnlyList<ApplicationUserId>> GetUserIdsInRoleAsync(string roleName, int max, CancellationToken cancellationToken)
    {
        return inner.GetUserIdsInRoleAsync(roleName, max, cancellationToken);
    }

    public Task<IReadOnlyList<KeycloakUser>> GetUsersInRoleAsync(string roleName, int skip, int take, CancellationToken cancellationToken)
    {
        return inner.GetUsersInRoleAsync(roleName, skip, take, cancellationToken);
    }

    public Task<IReadOnlyList<KeycloakUserSession>> GetUserSessionsAsync(ApplicationUserId userId, CancellationToken cancellationToken)
    {
        return inner.GetUserSessionsAsync(userId, cancellationToken);
    }

    public Task DeleteSessionAsync(string sessionId, CancellationToken cancellationToken)
    {
        return inner.DeleteSessionAsync(sessionId, cancellationToken);
    }

    public Task LogoutUserAsync(ApplicationUserId userId, CancellationToken cancellationToken)
    {
        return inner.LogoutUserAsync(userId, cancellationToken);
    }
}

internal sealed class InProcessSendOtpClient(IFusionCache cache, IOptions<OtpOptions> otpOptions)
    : IInterModuleRequestClient<SendPhoneOtpRequest, SendPhoneOtpResponse>
{
    public const string DummyOtp = "123456";

    public async Task<SendPhoneOtpResponse> SendAsync(
        SendPhoneOtpRequest request, CancellationToken cancellationToken)
    {
        var key = CacheKeys.For.Otp(request.PhoneNumber, request.Purpose, request.ContextId);
        var duration = TimeSpan.FromMinutes(otpOptions.Value.ExpirationInMinutes);
        var entry = new OtpCacheEntry(DummyOtp, 0, DateTimeOffset.UtcNow + duration);
        await cache.SetAsync(key, entry,
            new FusionCacheEntryOptions { Duration = duration },
            token: cancellationToken);
        return new SendPhoneOtpResponse(SmsOtpDispatchOutcome.Sent);
    }
}

internal sealed class InProcessVerifyOtpClient(IFusionCache cache)
    : IInterModuleRequestClient<VerifyPhoneOtpRequest, VerifyPhoneOtpResponse>
{
    private const int MaxFailedAttempts = 3;

    public async Task<VerifyPhoneOtpResponse> SendAsync(
        VerifyPhoneOtpRequest request, CancellationToken cancellationToken)
    {
        var key = CacheKeys.For.Otp(request.PhoneNumber, request.Purpose, request.ContextId);
        var entry = await cache.GetOrDefaultAsync<OtpCacheEntry>(key, token: cancellationToken);

        if (entry is null)
        {
            return new VerifyPhoneOtpResponse(OtpVerificationFailureReason.InvalidOtp);
        }

        if (!string.Equals(entry.Otp, request.Otp, StringComparison.Ordinal))
        {
            var failedAttempts = entry.FailedAttempts + 1;
            if (failedAttempts >= MaxFailedAttempts)
            {
                await cache.RemoveAsync(key, token: cancellationToken);
                return new VerifyPhoneOtpResponse(OtpVerificationFailureReason.TooManyAttempts);
            }

            var remaining = entry.ExpiresAt - DateTimeOffset.UtcNow;
            await cache.SetAsync(key, entry with { FailedAttempts = failedAttempts },
                new FusionCacheEntryOptions { Duration = remaining > TimeSpan.Zero ? remaining : TimeSpan.Zero },
                token: cancellationToken);
            return new VerifyPhoneOtpResponse(OtpVerificationFailureReason.InvalidOtp);
        }

        await cache.RemoveAsync(key, token: cancellationToken);
        return new VerifyPhoneOtpResponse(OtpVerificationFailureReason.None);
    }
}

internal sealed class InProcessSendEmailOtpClient(IFusionCache cache, IOptions<OtpOptions> otpOptions)
    : IInterModuleRequestClient<SendEmailOtpRequest, SendEmailOtpResponse>
{
    public const string DummyOtp = "123456";

    public async Task<SendEmailOtpResponse> SendAsync(
        SendEmailOtpRequest request, CancellationToken cancellationToken)
    {
        var key = CacheKeys.For.Otp(request.Email, request.Purpose, request.ContextId);
        var duration = TimeSpan.FromMinutes(otpOptions.Value.ExpirationInMinutes);
        var entry = new OtpCacheEntry(DummyOtp, 0, DateTimeOffset.UtcNow + duration);
        await cache.SetAsync(key, entry,
            new FusionCacheEntryOptions { Duration = duration },
            token: cancellationToken);
        return new SendEmailOtpResponse(EmailOtpDispatchOutcome.Sent);
    }
}

internal sealed class InProcessVerifyEmailOtpClient(IFusionCache cache)
    : IInterModuleRequestClient<VerifyEmailOtpRequest, VerifyEmailOtpResponse>
{
    private const int MaxFailedAttempts = 3;

    public async Task<VerifyEmailOtpResponse> SendAsync(
        VerifyEmailOtpRequest request, CancellationToken cancellationToken)
    {
        var key = CacheKeys.For.Otp(request.Email, request.Purpose, request.ContextId);
        var entry = await cache.GetOrDefaultAsync<OtpCacheEntry>(key, token: cancellationToken);

        if (entry is null)
        {
            return new VerifyEmailOtpResponse(OtpVerificationFailureReason.InvalidOtp);
        }

        if (!string.Equals(entry.Otp, request.Otp, StringComparison.Ordinal))
        {
            var failedAttempts = entry.FailedAttempts + 1;
            if (failedAttempts >= MaxFailedAttempts)
            {
                await cache.RemoveAsync(key, token: cancellationToken);
                return new VerifyEmailOtpResponse(OtpVerificationFailureReason.TooManyAttempts);
            }

            var remaining = entry.ExpiresAt - DateTimeOffset.UtcNow;
            await cache.SetAsync(key, entry with { FailedAttempts = failedAttempts },
                new FusionCacheEntryOptions { Duration = remaining > TimeSpan.Zero ? remaining : TimeSpan.Zero },
                token: cancellationToken);
            return new VerifyEmailOtpResponse(OtpVerificationFailureReason.InvalidOtp);
        }

        await cache.RemoveAsync(key, token: cancellationToken);
        return new VerifyEmailOtpResponse(OtpVerificationFailureReason.None);
    }
}

/// <summary>
///     Mirrors the real IssueVerificationTokenRequestHandler's shape (store under Identifier+Purpose) without
///     going through the CSPRNG path, so tests get a predictable-enough-to-log-but-still-unique token.
/// </summary>
internal sealed class InProcessIssueVerificationTokenClient(IFusionCache cache, IOptions<OtpOptions> otpOptions)
    : IInterModuleRequestClient<IssueVerificationTokenRequest, IssueVerificationTokenResponse>
{
    public async Task<IssueVerificationTokenResponse> SendAsync(
        IssueVerificationTokenRequest request, CancellationToken cancellationToken)
    {
        var token = Guid.NewGuid().ToString("N");
        var key = CacheKeys.For.Otp(request.Identifier, request.Purpose);
        var duration = TimeSpan.FromMinutes(otpOptions.Value.VerificationTokenExpirationInMinutes);
        await cache.SetAsync(key, new OtpCacheEntry(token, 0, DateTimeOffset.UtcNow + duration),
            new FusionCacheEntryOptions { Duration = duration },
            token: cancellationToken);
        return new IssueVerificationTokenResponse(token);
    }
}
