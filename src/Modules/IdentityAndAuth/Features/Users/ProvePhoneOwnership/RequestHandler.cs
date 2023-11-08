using Common.Core.Contracts.Results;
using IdentityAndAuth.Features.Users.Domain;
using IdentityAndAuth.Features.Users.Services.Otp;
using IdentityAndAuth.Features.Users.Services.PhoneVerificationToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NimbleMediator.Contracts;

namespace IdentityAndAuth.Features.Users.ProvePhoneOwnership;

internal sealed class RequestHandler(
        IOtpService otpService,
        IPhoneVerificationTokenService phoneVerificationTokenService,
        UserManager<ApplicationUser> userManager
    )
     : IRequestHandler<Request, Result<Response>>
{
    async ValueTask<Result<Response>> IRequestHandler<Request, Result<Response>>.HandleAsync(Request request, CancellationToken cancellationToken)
        => await otpService
            .ValidateAsync(request.Otp, request.PhoneNumber, cancellationToken)
            .BindAsync(phoneVerificationTokenService.GetTokenAsync(request.PhoneNumber, cancellationToken))
            .MapAsync(async token =>
            {
                var userExists = await UserExistsAsync(request.PhoneNumber, cancellationToken);
                return new Response(userExists, token);
            });

    private Task<bool> UserExistsAsync(string phoneNumber, CancellationToken cancellationToken)
        => userManager.Users.AnyAsync(x => x.PhoneNumber == phoneNumber, cancellationToken);
}
