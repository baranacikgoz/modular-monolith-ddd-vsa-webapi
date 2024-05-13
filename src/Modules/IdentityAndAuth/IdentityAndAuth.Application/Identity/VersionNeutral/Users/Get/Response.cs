using Common.Domain.StronglyTypedIds;

namespace IdentityAndAuth.Application.Identity.VersionNeutral.Users.Get;

internal sealed record Response(
    ApplicationUserId Id,
    string Name,
    string LastName,
    string PhoneNumber,
    string NationalIdentityNumber,
    DateOnly BirthDate);
