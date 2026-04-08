using System.Globalization;
using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using IAM.Application.Captcha.Services;
using IAM.Application.Extensions;
using IAM.Application.Otp.Services;
using IAM.Application.Persistence;
using IAM.Domain.Errors;
using IAM.Domain.Identity;
using IAM.Infrastructure.Telemetry;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
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
            .TransformResultTo<Response>();
    }

    private static Task<Result<Response>> RegisterAsync(
        Request request,
        UserManager<ApplicationUser> userManager,
        IOtpService otpService,
        ICaptchaService captchaService,
        IIAMDbContext dbContext,
        CancellationToken cancellationToken)
    {
        // Captcha validation is optional: validate only when a token is supplied.
        // This allows backward-compatible rollout across environments.
        return (string.IsNullOrEmpty(request.CaptchaToken)
                ? Task.FromResult(Result.Success)
                : captchaService.ValidateAsync(request.CaptchaToken, cancellationToken))
            .BindAsync(() => CreateUserPipelineAsync(request, userManager, otpService, dbContext, cancellationToken));
    }

    private static Task<Result<Response>> CreateUserPipelineAsync(
        Request request,
        UserManager<ApplicationUser> userManager,
        IOtpService otpService,
        IIAMDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return otpService
            .VerifyThenRemoveOtpAsync(request.PhoneNumber, request.Otp, cancellationToken)
            .BindAsync(async () =>
            {
                // Explicit phone uniqueness check before handing off to Identity — avoids
                // the cryptic DuplicateUserName error and returns a domain-friendly error instead.
                var alreadyExists = await dbContext
                    .Users
                    .AsNoTracking()
                    .AnyAsync(u => u.PhoneNumber == request.PhoneNumber, cancellationToken);

                return alreadyExists
                    ? Result<ApplicationUser>.Failure(IdentityErrors.PhoneNumberAlreadyRegistered)
                    : Result<ApplicationUser>.Success(ApplicationUser.Create(
                        request.Name,
                        request.LastName,
                        request.PhoneNumber,
                        request.NationalIdentityNumber,
                        DateOnly.ParseExact(request.BirthDate, Constants.TurkishDateFormat,
                            CultureInfo.InvariantCulture)));
            })
            .BindAsync(async user =>
            {
                var identityResult = await userManager.CreateAsync(user);
                return identityResult.ToResult(user);
            })
            .BindAsync(async user =>
            {
                var identityResult = await userManager.AddToRoleAsync(user, CustomRoles.Basic);
                return identityResult.ToResult(user);
            })
            .TapAsync(_ => IamTelemetry.UsersRegistered.Add(1))
            .MapAsync(user => new Response { Id = user.Id });
    }
}
