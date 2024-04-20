using Common.Core.Contracts.Identity;

namespace IdentityAndAuth.Features.Identity.UseCases.Users.Get;

internal sealed record Response(
    ApplicationUserId Id,
    string Name,
    string LastName,
    string PhoneNumber,
    string NationalIdentityNumber,
    DateOnly BirthDate);
