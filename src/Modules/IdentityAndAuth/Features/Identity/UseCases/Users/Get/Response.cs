namespace IdentityAndAuth.Features.Identity.UseCases.Users.Get;

internal sealed record Response(
    Guid Id,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string NationalIdentityNumber,
    DateOnly BirthDate);
