using System.Globalization;
using Common.Core.Contracts.Results;
using Common.DomainEvents;
using Common.Eventbus;
using IdentityAndAuth.Extensions;
using IdentityAndAuth.Features.Auth.Domain;
using IdentityAndAuth.Features.Identity.Domain;
using Microsoft.AspNetCore.Identity;
using NimbleMediator.Contracts;

namespace IdentityAndAuth.Features.Identity.UseCases.Users.SelfRegister;

internal sealed class RequestHandler(
        IPhoneVerificationTokenService phoneVerificationTokenService,
        UserManager<ApplicationUser> userManager,
        IEventBus eventBus
        ) : IRequestHandler<Request, Result<Response>>
{
    public async ValueTask<Result<Response>> HandleAsync(Request request, CancellationToken cancellationToken)
        => await phoneVerificationTokenService
                .ValidateTokenAsync(request.PhoneNumber, request.PhoneVerificationToken, cancellationToken)
                .BindAsync(CreateUserAsync(request))
                .BindAsync(async user =>
                {
                    var assignRoleResult = await AssignRoleToUserAsync(user, CustomRoles.Basic);
                    return assignRoleResult.IsSuccess
                        ? Result<ApplicationUser>.Success(user)
                        : Result<ApplicationUser>.Failure(assignRoleResult.Error!);
                })
                .BindAsync(async (ApplicationUser user) =>
                {
                    // We would normally add event in Aggregate's DomainEvents then publish in SaveChangesAsync (DbContext),
                    // but I am not sure how we can do this with UserManager.
                    // So published event here. Any idea to make it better?
                    await eventBus.PublishAsync(new Events
                                                    .FromIdentityAndAuth
                                                    .UserCreatedEvent(user.Id));

                    return Result<ApplicationUser>.Success(user);
                })
                .MapAsync((ApplicationUser user) => new Response(user.Id));

    private async Task<Result<ApplicationUser>> CreateUserAsync(Request request)
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

    private async Task<Result> AssignRoleToUserAsync(ApplicationUser user, string role)
    {
        var identityResult = await userManager.AddToRoleAsync(user, role);
        return identityResult.ToResult();
    }
}
