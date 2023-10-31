using Common.Core.Contracts.Results;
using NimbleMediator.Contracts;

namespace IdentityAndAuth.Features.Users.InitiatePhoneOwnershipProcess;
public sealed record Request(string PhoneNumber) : IRequest<Result>;
