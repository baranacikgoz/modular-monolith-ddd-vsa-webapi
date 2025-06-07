using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using IAM.Application.Auth;
using IAM.Application.Extensions;
using IAM.Application.Otp.Services;
using IAM.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace IAM.Endpoints.Users.VersionNeutral.SelfRegister;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapPost("register/self", RegisterAsync)
            .WithDescription("Register a new user.")
            .Produces<Response>(StatusCodes.Status200OK)
            .AllowAnonymous()
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> RegisterAsync(
        [FromBody] Request request,
        [FromServices] UserManager<ApplicationUser> userManager,
        [FromServices] IOtpService otpService,
        CancellationToken cancellationToken)
        => await otpService
            .VerifyThenRemoveOtpAsync(request.PhoneNumber, request.Otp, cancellationToken)
            .BindAsync(() => ApplicationUser.Create(
                request.Name,
                request.LastName,
                request.PhoneNumber,
                request.NationalIdentityNumber,
                DateOnly.ParseExact(request.BirthDate, Domain.Constants.TurkishDateFormat, CultureInfo.InvariantCulture)))
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
            .MapAsync(user => new Response { Id = user.Id });
}
