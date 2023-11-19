using Common.Core.Auth;
using Common.Core.Contracts.Results;
using IdentityAndAuth.Features.Identity.Domain;
using IdentityAndAuth.Features.Identity.Domain.Errors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NimbleMediator.Contracts;

namespace IdentityAndAuth.Features.Identity.UseCases.Users.Current.Get;

internal sealed class RequestHandler(
    ICurrentUser currentUser,
    UserManager<ApplicationUser> userManager
    ) : IRequestHandler<Request, Result<Response>>
{
    public async ValueTask<Result<Response>> HandleAsync(Request request, CancellationToken cancellationToken)
        => await Result<Dto>.Create(
                taskToAwaitValue: async () => await userManager
                                                    .Users
                                                    .Where(x => x.Id == currentUser.Id)
                                                    .Select(x => new Dto(x.Id, x.FirstName, x.LastName, x.PhoneNumber!))
                                                    .SingleOrDefaultAsync(cancellationToken),
                ifTaskReturnsNull: UserErrors.UserNotFound)
            .MapAsync(dto => new Response(
                                    dto.Id,
                                    dto.FirstName,
                                    dto.LastName,
                                    dto.PhoneNumber));

    private sealed record Dto(
        Guid Id,
        string FirstName,
        string LastName,
        string PhoneNumber
    );
}
