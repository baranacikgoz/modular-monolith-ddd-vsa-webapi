using System.Globalization;
using Common.Core.Contracts.Results;
using Common.Eventbus;
using IdentityAndAuth.Auth;
using IdentityAndAuth.Extensions;
using IdentityAndAuth.Features.Users.Domain;
using IdentityAndAuth.Features.Users.Services.PhoneVerificationToken;
using Microsoft.AspNetCore.Identity;
using NimbleMediator.Contracts;
using Common.DomainEvents.viaIdentityAndAuth;

namespace IdentityAndAuth.Features.Users.SelfRegister;

internal sealed class RequestHandler(
        IPhoneVerificationTokenService phoneVerificationTokenService,
        UserManager<ApplicationUser> userManager,
        IEventBus eventBus
        ) : IRequestHandler<Request, Result<Response>>
{
    public async ValueTask<Result<Response>> HandleAsync(Request request, CancellationToken cancellationToken)
        => await phoneVerificationTokenService
                .ValidateTokenAsync(request.PhoneNumber, request.PhoneVerificationToken, cancellationToken)
                .BindAsync(() => CreateUserAndAssignRoleAsync(request))
                .BindAsync(async (Response response) =>
                {
                    await eventBus.PublishAsync(new Events.IdentityAndAuth.UserCreatedEvent(response.Id));

                    return Result<Response>.Success(response);
                });

    private async Task<Result<Response>> CreateUserAndAssignRoleAsync(Request request)
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

        return await identityResult.ToResult()
            .BindAsync(() => AssignRoleToUserAsync(user, CustomRoles.Basic))
            .MapAsync(() => new Response(user.Id));
    }

    private async Task<Result> AssignRoleToUserAsync(ApplicationUser user, string role)
    {
        var identityResult = await userManager.AddToRoleAsync(user, role);
        return identityResult.ToResult();
    }
}
