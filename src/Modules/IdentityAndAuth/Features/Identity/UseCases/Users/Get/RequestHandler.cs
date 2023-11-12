using Common.Core.Contracts.Results;
using IdentityAndAuth.Features.Identity.Domain;
using IdentityAndAuth.Features.Identity.Domain.Errors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NimbleMediator.Contracts;

namespace IdentityAndAuth.Features.Identity.UseCases.Users.Get;

internal sealed class RequestHandler(UserManager<ApplicationUser> userManager) : IRequestHandler<Request, Result<Response>>
{
    public async ValueTask<Result<Response>> HandleAsync(Request request, CancellationToken cancellationToken)
    {
        var response = await userManager
                        .Users
                        .Where(x => x.Id == request.Id)
                        .Select(x => new Response(x.Id, x.FirstName, x.LastName, x.PhoneNumber!, x.NationalIdentityNumber, x.BirthDate))
                        .SingleOrDefaultAsync(cancellationToken);

        if (response is null)
        {
            return UserErrors.NotFound;
        }

        return response;
    }
}
