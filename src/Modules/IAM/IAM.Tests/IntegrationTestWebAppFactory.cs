using Common.Application.Caching;
using Common.Application.Options;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Notifications;
using Common.Tests;
using IAM.Application.Captcha.Services;
using IAM.Infrastructure.Captcha.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ZiggyCreatures.Caching.Fusion;

namespace IAM.Tests;

public class IntegrationTestWebAppFactory : IntegrationTestFactory
{
    protected override string[] GetActiveModules()
    {
        return ["IAM", "Outbox"];
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureAppConfiguration((_, config) =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string?>
            {
                { "FeatureManagement:IAM.Captcha", "true" }
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
        });
    }
}

internal sealed class InProcessSendOtpClient(IFusionCache cache, IOptions<OtpOptions> otpOptions)
    : IInterModuleRequestClient<SendPhoneOtpRequest, SendPhoneOtpResponse>
{
    private const string DummyOtp = "123456";

    public async Task<SendPhoneOtpResponse> SendAsync(
        SendPhoneOtpRequest request, CancellationToken cancellationToken)
    {
        var key = CacheKeys.For.Otp(request.PhoneNumber, request.Purpose, request.ContextId);
        var duration = TimeSpan.FromMinutes(otpOptions.Value.ExpirationInMinutes);
        var entry = new OtpCacheEntry(DummyOtp, 0, DateTimeOffset.UtcNow + duration);
        await cache.SetAsync(key, entry,
            new FusionCacheEntryOptions { Duration = duration },
            token: cancellationToken);
        return new SendPhoneOtpResponse();
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
