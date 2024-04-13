using System.Globalization;
using Common.Core.Contracts.Results;
using Common.EventBus.Contracts;
using IdentityAndAuth.Extensions;
using IdentityAndAuth.Features.Auth.Domain;
using IdentityAndAuth.Features.Identity.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Common.Core.Extensions;

namespace IdentityAndAuth.Features.Identity.UseCases.Users.SelfRegister;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapPost("self-register", SelfRegisterAsync)
            .WithDescription("Self register a new user.")
            .Produces<Response>(StatusCodes.Status200OK)
            .AllowAnonymous()
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> SelfRegisterAsync(
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
                    DateOnly.ParseExact(request.BirthDate, SelfRegister.Constants.TurkishDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None)))
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
