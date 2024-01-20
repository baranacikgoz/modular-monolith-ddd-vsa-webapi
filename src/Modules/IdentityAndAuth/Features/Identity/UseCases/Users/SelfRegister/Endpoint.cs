using System.Globalization;
using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using Common.Core.EndpointFilters;
using Common.Eventbus;
using IdentityAndAuth.Extensions;
using IdentityAndAuth.Features.Auth.Domain;
using IdentityAndAuth.Features.Identity.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using NimbleMediator.Contracts;
using Common.DomainEvents;

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
        [FromServices] IEventBus eventBus,
        CancellationToken cancellationToken)
        => await ApplicationUser.CreateAsync(
                new()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PhoneNumber = request.PhoneNumber,
                    NationalIdentityNumber = request.NationalIdentityNumber,
                    BirthDate = DateOnly.ParseExact(request.BirthDate, SelfRegister.Constants.TurkishDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None),
                },
                phoneVerificationTokenService,
                request.PhoneVerificationToken,
                cancellationToken)
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
            .BindAsync(async user => await eventBus.PublishAsync(new Events
                                                                    .Published
                                                                    .From
                                                                    .IdentityAndAuth
                                                                    .UserCreated(user.Id, user.Name)))
            .MapAsync(user => new Response(user.Id));
}
