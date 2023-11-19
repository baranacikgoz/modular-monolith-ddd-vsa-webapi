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
            .Produces<Response>()
            .AllowAnonymous()
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> SelfRegisterAsync(
        [FromBody] Request request,
        [FromServices] IPhoneVerificationTokenService phoneVerificationTokenService,
        [FromServices] UserManager<ApplicationUser> userManager,
        [FromServices] IEventBus eventBus,
        CancellationToken cancellationToken)
        => await phoneVerificationTokenService
            .ValidateTokenAsync(request.PhoneNumber, request.PhoneVerificationToken, cancellationToken)
            .BindAsync(async () => await CreateUserAsync(userManager, request))
            .BindAsync(async user => await AssignRoleToUserAsync(userManager, user, CustomRoles.Basic))
            .BindAsync(async user => await eventBus.PublishAsync(new Events.FromIdentityAndAuth.UserCreatedEvent(user.Id)))
            .MapAsync(user => new Response(user.Id));

    private static async Task<Result<ApplicationUser>> CreateUserAsync(
        UserManager<ApplicationUser> userManager,
        Request request)
    {
        var user = ApplicationUser.Create(
            new()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                NationalIdentityNumber = request.NationalIdentityNumber,
                BirthDate = DateOnly.Parse(request.BirthDate, CultureInfo.InvariantCulture)
            }
        );

        var identityResult = await userManager.CreateAsync(user);

        return identityResult.ToResult(user);
    }

    private static async Task<Result> AssignRoleToUserAsync(
        UserManager<ApplicationUser> userManager,
        ApplicationUser user,
        string role)
    {
        var identityResult = await userManager.AddToRoleAsync(user, role);
        return identityResult.ToResult();
    }
}
