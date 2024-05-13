using Common.Domain.ResultMonad;
using IdentityAndAuth.Application.Identity.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Common.Application.Extensions;
using IdentityAndAuth.Domain.Identity;

namespace IdentityAndAuth.Application.Identity.VersionNeutral.Users.ProvePhoneOwnership;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapPost("prove-phone-ownership", ProvePhoneOwnershipAsync)
            .WithDescription("Prove phone ownership by validating otp.")
            .Produces<Response>(StatusCodes.Status200OK)
            .AllowAnonymous()
            .TransformResultTo<Response>();
    }
    private static async Task<Result<Response>> ProvePhoneOwnershipAsync(
        [FromBody] Request request,
        [FromServices] IOtpService otpService,
        [FromServices] IPhoneVerificationTokenService phoneVerificationTokenService,
        [FromServices] UserManager<ApplicationUser> userManager,
        CancellationToken cancellationToken)
        => await otpService
            .ValidateAsync(request.Otp, request.PhoneNumber, cancellationToken)
            .BindAsync(async () => await phoneVerificationTokenService.GetTokenAsync(request.PhoneNumber, cancellationToken))
            .MapAsync(async phoneVerificationToken => new Response(
                                                        UserExists: await IsUserExistAsync(userManager, request.PhoneNumber, cancellationToken),
                                                        PhoneVerificationToken: phoneVerificationToken));

    private static Task<bool> IsUserExistAsync(
        UserManager<ApplicationUser> userManager,
        string phoneNumber,
        CancellationToken cancellationToken)
        => userManager
            .Users
            .AnyAsync(x => x.PhoneNumber == phoneNumber, cancellationToken);
}
