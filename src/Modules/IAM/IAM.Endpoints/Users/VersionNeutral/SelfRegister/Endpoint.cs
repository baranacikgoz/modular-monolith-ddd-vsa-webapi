using System.Globalization;
using System.Security.Cryptography;
using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.FeatureManagement;
using Common.Application.Options;
using Common.Domain.ResultMonad;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Notifications;
using IAM.Application.Captcha.Services;
using IAM.Application.Extensions;
using IAM.Application.Persistence;
using IAM.Application.Tokens.Services;
using IAM.Domain.Identity;
using IAM.Endpoints.Otp;
using IAM.Infrastructure.Telemetry;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;
using Constants = IAM.Domain.Constants;

namespace IAM.Endpoints.Users.VersionNeutral.SelfRegister;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapPost("register/self", RegisterAsync)
            .WithDescription("Register a new user.")
            .Produces<Response>()
            .AllowAnonymous()
            .RequireRateLimiting(Infrastructure.RateLimiting.Constants.Register)
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> RegisterAsync(
        Request request,
        UserManager<ApplicationUser> userManager,
        IInterModuleRequestClient<VerifyPhoneOtpRequest, VerifyPhoneOtpResponse> otpClient,
        ICaptchaService captchaService,
        IIAMDbContext db,
        ITokenService tokenService,
        TimeProvider timeProvider,
        IFeatureManager featureManager,
        IOptions<JwtOptions> jwtOptionsProvider,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var captchaTask = await featureManager.IsEnabledAsync(FeatureFlags.IAM.Captcha)
            ? captchaService.ValidateAsync(request.CaptchaToken ?? string.Empty, cancellationToken)
            : Task.FromResult(Result.Success);

        return await captchaTask
            .BindAsync(() => RegisterAndLoginAsync(request, userManager, otpClient, db, tokenService, timeProvider,
                jwtOptionsProvider, httpContext, cancellationToken));
    }

    private static async Task<Result<Response>> RegisterAndLoginAsync(
        Request request,
        UserManager<ApplicationUser> userManager,
        IInterModuleRequestClient<VerifyPhoneOtpRequest, VerifyPhoneOtpResponse> otpClient,
        IIAMDbContext db,
        ITokenService tokenService,
        TimeProvider timeProvider,
        IOptions<JwtOptions> jwtOptionsProvider,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var verifyOtpResponse = await otpClient.SendAsync(
            new VerifyPhoneOtpRequest(request.PhoneNumber, request.Otp, OtpPurposes.Registration),
            cancellationToken);

        // One transaction for the whole user + role + session sequence: userManager.CreateAsync
        // and userManager.AddToRoleAsync each call SaveChanges internally, but both share this
        // DbContext/connection, so they participate in the same ambient transaction as the final
        // db.SaveChangesAsync below. A failure anywhere in the chain rolls back everything instead
        // of leaving a half-registered user (no role/session) that a retry can't recover from.
        await using var transaction = await db.Database.BeginTransactionAsync(cancellationToken);

        return await verifyOtpResponse
            .ToResult()
            .BindAsync(async () =>
            {
                var user = ApplicationUser.Create(
                    request.FullName,
                    request.PhoneNumber,
                    DateOnly.ParseExact(request.BirthDate, Constants.TurkishDateFormat, CultureInfo.InvariantCulture));
                var identityResult = await userManager.CreateAsync(user);
                return identityResult.ToResult(user);
            })
            .BindAsync(async user =>
            {
                var identityResult = await userManager.AddToRoleAsync(user, CustomRoles.Basic);
                return identityResult.ToResult(user);
            })
            .BindAsync(async user =>
            {
                var utcNow = timeProvider.GetUtcNow();
                var (refreshTokenBytes, refreshTokenExpiresAt) = tokenService.GenerateRefreshToken(utcNow);
                var sessionAbsoluteExpiresAt = utcNow.AddDays(jwtOptionsProvider.Value.SessionAbsoluteExpirationInDays);
                var ip = httpContext.Connection.RemoteIpAddress?.ToString();
                var userAgent = httpContext.Request.Headers.UserAgent.ToString();

                // Brand-new user — there is no existing session to reuse.
                var refreshToken = user.IssueSessionAndToken(
                    existingSession: null, request.DeviceId, request.ClientId, request.DeviceName, ip, userAgent,
                    SHA256.HashData(refreshTokenBytes), utcNow, refreshTokenExpiresAt, sessionAbsoluteExpiresAt);

                var (accessToken, accessTokenExpiresAt) =
                    tokenService.GenerateAccessToken(utcNow, user.Id, refreshToken.SessionId, [CustomRoles.Basic]);

                await db.SaveChangesAsync(cancellationToken);
                IamTelemetry.SessionsCreated.Add(1);

                return Result<Response>.Success(new Response
                {
                    AccessToken = accessToken,
                    AccessTokenExpiresAt = accessTokenExpiresAt,
                    RefreshToken = Convert.ToBase64String(refreshTokenBytes),
                    RefreshTokenExpiresAt = refreshTokenExpiresAt
                });
            })
            .TapAsync(async _ => await transaction.CommitAsync(cancellationToken))
            .TapAsync(_ => IamTelemetry.UsersRegistered.Add(1))
            .TapAsync(_ => IamTelemetry.Logins.Add(1));
    }
}
