using System.Globalization;
using Common.Application.Caching;
using Common.Application.Options;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Notifications;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Notifications.Application.Otp;
using Notifications.Application.Sms;
using Notifications.Infrastructure.Telemetry;
using ZiggyCreatures.Caching.Fusion;

namespace Notifications.Infrastructure.InterModuleRequestHandlers;

public sealed class SendPhoneOtpRequestHandler(
    IOtpService otpService,
    ISmsGateway smsGateway,
    IFusionCache cache,
    IOptions<OtpOptions> otpOptionsProvider,
    IOptions<SmsOptions> smsOptionsProvider,
    IOptions<RequestLocalizationOptions> localizationOptionsProvider
) : InterModuleRequestHandler<SendPhoneOtpRequest, SendPhoneOtpResponse>
{
    public override async Task<SendPhoneOtpResponse> HandleAsync(
        SendPhoneOtpRequest request,
        CancellationToken cancellationToken)
    {
        var opts = otpOptionsProvider.Value;

        var resendKey = CacheKeys.For.OtpResend(request.PhoneNumber, request.Purpose, request.ContextId);
        if (await cache.GetOrDefaultAsync<bool>(resendKey, token: cancellationToken))
        {
            return new SendPhoneOtpResponse(SmsOtpDispatchOutcome.Throttled);
        }

        // Phone-only quota, deliberately independent of Purpose/ContextId: the resend guard above is
        // per-context and the IP rate limiter is per-caller, so rotating either gives unlimited real SMS
        // to one phone number. This is the backstop that still holds when that happens.
        var phoneQuotaKey = CacheKeys.For.OtpPhoneQuota(request.PhoneNumber);
        var priorSends = await cache.GetOrDefaultAsync<int>(phoneQuotaKey, token: cancellationToken);
        if (priorSends >= opts.MaxSendsPerPhonePerWindow)
        {
            return new SendPhoneOtpResponse(SmsOtpDispatchOutcome.Throttled);
        }

        var templates = smsOptionsProvider.Value.Templates.Otp;
        var language = request.Language
                       ?? localizationOptionsProvider.Value.DefaultRequestCulture.UICulture.TwoLetterISOLanguageName;

        if (!templates.TryGetValue(language, out var template))
        {
            var fallback = localizationOptionsProvider.Value.DefaultRequestCulture.UICulture.TwoLetterISOLanguageName;
            if (!templates.TryGetValue(fallback, out template))
            {
                throw new InvalidOperationException(
                    $"No SmsTemplate configured for language '{language}' or default language '{fallback}'. Add an entry to SmsOptions:Templates:Otp.");
            }
        }

        var otp = otpService.Generate();
        await otpService.StoreAsync(
            request.PhoneNumber,
            otp,
            request.Purpose,
            TimeSpan.FromMinutes(opts.ExpirationInMinutes),
            request.ContextId,
            cancellationToken);
        var message = string.Format(CultureInfo.InvariantCulture, template, otp);
        var sendResult = await smsGateway.SendAsync(
            new SmsMessage(request.PhoneNumber, message, SmsCategory.Transactional), cancellationToken);

        if (sendResult.IsFailure)
        {
            var outcome = sendResult.Error!.Key == SmsErrors.Throttled.Key
                ? SmsOtpDispatchOutcome.Throttled
                : SmsOtpDispatchOutcome.ProviderUnavailable;
            return new SendPhoneOtpResponse(outcome);
        }

        // Resend guard set only after a successful send: a failed send must not lock the user out
        // for ResendIntervalSeconds; the stored-but-undelivered OTP simply expires on its own.
        await cache.SetAsync(
            resendKey,
            true,
            new FusionCacheEntryOptions { Duration = TimeSpan.FromSeconds(opts.ResendIntervalSeconds) },
            cancellationToken);

        // ponytail: read-modify-write, not atomic. Concurrent requests for the same phone can overshoot
        // the cap by a few sends. Acceptable for a spend cap; upgrade to a Redis INCR+PEXPIRE Lua script
        // if tighter enforcement is ever needed. Only incremented after a successful send, matching the
        // resend guard above.
        await cache.SetAsync(
            phoneQuotaKey,
            priorSends + 1,
            new FusionCacheEntryOptions { Duration = TimeSpan.FromMinutes(opts.PhoneQuotaWindowMinutes) },
            cancellationToken);
        NotificationsTelemetry.RecordOtpSent(request.Purpose);
        return new SendPhoneOtpResponse(SmsOtpDispatchOutcome.Sent);
    }
}
