using Common.Domain.ResultMonad;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Common.Application.Extensions;
using IAM.Domain.Identity;
using IAM.Application.Identity.Services;

namespace IAM.Application.Identity.VersionNeutral.Users.VerifyOtp;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapPost("verify-otp", VerifyOtpAsync)
            .WithDescription("Verify otp sms.")
            .Produces<Response>(StatusCodes.Status200OK)
            .AllowAnonymous()
            .TransformResultTo<Response>();
    }
    private static async Task<Result<Response>> VerifyOtpAsync(
        [FromBody] Request request,
        [FromServices] IOtpService otpService,
        [FromServices] IPhoneVerificationTokenService phoneVerificationTokenService,
        [FromServices] UserManager<ApplicationUser> userManager,
        CancellationToken cancellationToken)
        => await otpService
            .ValidateAsync(request.Otp, request.PhoneNumber, cancellationToken)
            .BindAsync(async () => await phoneVerificationTokenService.GetTokenAsync(request.PhoneNumber, cancellationToken))
            .MapAsync(async phoneVerificationToken => new Response
            {
                IsRegistered = await IsRegisteredAsync(userManager, request.PhoneNumber, cancellationToken),
                PhoneVerificationToken = phoneVerificationToken
            });

    private static Task<bool> IsRegisteredAsync(
        UserManager<ApplicationUser> userManager,
        string phoneNumber,
        CancellationToken cancellationToken)
        => userManager
            .Users
            .AnyAsync(x => x.PhoneNumber == phoneNumber, cancellationToken);
}
