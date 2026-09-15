using System.Globalization;
using Common.Application.Caching;
using Common.Application.Options;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Notifications;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Notifications.Application.Email;
using Notifications.Application.Otp;
using Notifications.Infrastructure.Telemetry;
using ZiggyCreatures.Caching.Fusion;

namespace Notifications.Infrastructure.InterModuleRequestHandlers;

public sealed class SendEmailOtpRequestHandler(
    IOtpService otpService,
    IEmailGateway emailGateway,
    IFusionCache cache,
    IOptions<OtpOptions> otpOptionsProvider,
    IOptions<EmailOptions> emailOptionsProvider,
    IOptions<RequestLocalizationOptions> localizationOptionsProvider
) : InterModuleRequestHandler<SendEmailOtpRequest, SendEmailOtpResponse>
{
    public override async Task<SendEmailOtpResponse> HandleAsync(
        SendEmailOtpRequest request,
        CancellationToken cancellationToken)
    {
        var opts = otpOptionsProvider.Value;

        var resendKey = CacheKeys.For.OtpResend(request.Email, request.Purpose, request.ContextId);
        if (await cache.GetOrDefaultAsync<bool>(resendKey, token: cancellationToken))
        {
            return new SendEmailOtpResponse(EmailOtpDispatchOutcome.Throttled);
        }

        // Email-only quota, deliberately independent of Purpose/ContextId: the resend guard above is
        // per-context and the IP rate limiter is per-caller, so rotating either gives unlimited real
        // emails to one address. This is the backstop that still holds when that happens.
        var emailQuotaKey = CacheKeys.For.OtpEmailQuota(request.Email);
        var priorSends = await cache.GetOrDefaultAsync<int>(emailQuotaKey, token: cancellationToken);
        if (priorSends >= opts.MaxSendsPerEmailPerWindow)
        {
            return new SendEmailOtpResponse(EmailOtpDispatchOutcome.Throttled);
        }

        var templates = emailOptionsProvider.Value.Templates.Otp;
        var language = request.Language
                       ?? localizationOptionsProvider.Value.DefaultRequestCulture.UICulture.TwoLetterISOLanguageName;

        if (!templates.TryGetValue(language, out var template))
        {
            var fallback = localizationOptionsProvider.Value.DefaultRequestCulture.UICulture.TwoLetterISOLanguageName;
            if (!templates.TryGetValue(fallback, out template))
            {
                throw new InvalidOperationException(
                    $"No EmailTemplate configured for language '{language}' or default language '{fallback}'. Add an entry to EmailOptions:Templates:Otp.");
            }
        }

        var otp = otpService.Generate();
        await otpService.StoreAsync(
            request.Email,
            otp,
            request.Purpose,
            TimeSpan.FromMinutes(opts.ExpirationInMinutes),
            request.ContextId,
            cancellationToken);

        var message = new EmailMessage(
            request.Email,
            string.Format(CultureInfo.InvariantCulture, template.Subject, otp),
            string.Format(CultureInfo.InvariantCulture, template.HtmlBody, otp),
            template.TextBody is null ? null : string.Format(CultureInfo.InvariantCulture, template.TextBody, otp));
        var sendResult = await emailGateway.SendAsync(message, cancellationToken);

        if (sendResult.IsFailure)
        {
            var outcome = sendResult.Error!.Key == EmailErrors.Throttled.Key
                ? EmailOtpDispatchOutcome.Throttled
                : EmailOtpDispatchOutcome.ProviderUnavailable;
            return new SendEmailOtpResponse(outcome);
        }

        // Resend guard set only after a successful send: a failed send must not lock the user out
        // for ResendIntervalSeconds; the stored-but-undelivered OTP simply expires on its own.
        await cache.SetAsync(
            resendKey,
            true,
            new FusionCacheEntryOptions { Duration = TimeSpan.FromSeconds(opts.ResendIntervalSeconds) },
            cancellationToken);

        // ponytail: read-modify-write, not atomic. Concurrent requests for the same address can overshoot
        // the cap by a few sends. Acceptable for a spend cap; upgrade to a Redis INCR+PEXPIRE Lua script
        // if tighter enforcement is ever needed. Only incremented after a successful send, matching the
        // resend guard above.
        await cache.SetAsync(
            emailQuotaKey,
            priorSends + 1,
            new FusionCacheEntryOptions { Duration = TimeSpan.FromMinutes(opts.EmailQuotaWindowMinutes) },
            cancellationToken);
        NotificationsTelemetry.RecordOtpSent(request.Purpose);
        return new SendEmailOtpResponse(EmailOtpDispatchOutcome.Sent);
    }
}
