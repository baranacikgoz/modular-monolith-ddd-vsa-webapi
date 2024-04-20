using Common.Core.Contracts.Identity;

namespace IdentityAndAuth.Features.Identity.UseCases.Users.Current.Get;

public sealed record Response(ApplicationUserId Id, string Name, string LastName, string PhoneNumber);
