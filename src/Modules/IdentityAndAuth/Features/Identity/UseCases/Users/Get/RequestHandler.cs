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
        => await Result<Dto>
                .Create(taskToAwaitValue: async () => await userManager
                                                            .Users
                                                            .Where(x => x.Id == request.Id)
                                                            .Select(x => new Dto(x.Id, x.FirstName, x.LastName, x.PhoneNumber!, x.NationalIdentityNumber, x.BirthDate))
                                                            .SingleOrDefaultAsync(cancellationToken),
                        ifTaskReturnsNull: UserErrors.UserNotFound)
                .MapAsync(dto => new Response(
                                        dto.Id,
                                        dto.FirstName,
                                        dto.LastName,
                                        dto.PhoneNumber,
                                        dto.NationalIdentityNumber,
                                        dto.BirthDate));

    private sealed record Dto(
        Guid Id,
        string FirstName,
        string LastName,
        string PhoneNumber,
        string NationalIdentityNumber,
        DateOnly BirthDate
    );
}
