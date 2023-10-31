using Common.Core.Auth;
using Common.Core.Contracts.Results;
using IdentityAndAuth.Features.Users.Domain;
using IdentityAndAuth.Features.Users.Domain.Errors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NimbleMediator.Contracts;

namespace IdentityAndAuth.Features.Users.Current.Get;

internal sealed class RequestHandler(
    ICurrentUser currentUser,
    UserManager<ApplicationUser> userManager
    ) : IRequestHandler<Request, Result<Response>>
{
    public async ValueTask<Result<Response>> HandleAsync(Request request, CancellationToken cancellationToken)
    {
        var id = currentUser.Id;
        if (id == Guid.Empty)
        {
            return UserErrors.NotFound;
        }

        var user = await userManager
                        .Users
                        .Where(x => x.Id == id)
                        .Select(x => new Response(x.FirstName, x.LastName, x.PhoneNumber!))
                        .SingleOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            return UserErrors.NotFound;
        }

        return user;
    }
}
