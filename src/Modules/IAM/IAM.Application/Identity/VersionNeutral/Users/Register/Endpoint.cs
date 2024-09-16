using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using IAM.Domain.Identity;
using IAM.Application.Identity.Services;
using IAM.Application.Auth;
using IAM.Application.Extensions;

namespace IAM.Application.Identity.VersionNeutral.Users.Register;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapPost("register", RegisterAsync)
            .WithDescription("Register a new user.")
            .Produces<Response>(StatusCodes.Status200OK)
            .AllowAnonymous()
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> RegisterAsync(
        [FromBody] Request request,
        [FromServices] IPhoneVerificationTokenService phoneVerificationTokenService,
        [FromServices] UserManager<ApplicationUser> userManager,
        CancellationToken cancellationToken)
        => await phoneVerificationTokenService
                .ValidateTokenAsync(request.PhoneNumber, request.PhoneVerificationToken, cancellationToken)
                .MapAsync(() => ApplicationUser.Create(
                    request.Name,
                    request.LastName,
                    request.PhoneNumber,
                    request.NationalIdentityNumber,
                    DateOnly.ParseExact(request.BirthDate, Constants.TurkishDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None)))
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
                .MapAsync(user => new Response(user.Id));
}
