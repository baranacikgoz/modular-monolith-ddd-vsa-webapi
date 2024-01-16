namespace IdentityAndAuth.Features.Identity.UseCases.Users.Current.Get;

public sealed record Response(Guid Id, string Name, string LastName, string PhoneNumber);
