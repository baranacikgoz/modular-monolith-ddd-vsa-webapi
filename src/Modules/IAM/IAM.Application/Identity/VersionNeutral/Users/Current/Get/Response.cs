using Common.Domain.StronglyTypedIds;

namespace IAM.Application.Identity.VersionNeutral.Users.Current.Get;

public sealed record Response(ApplicationUserId Id, string Name, string LastName, string PhoneNumber);
