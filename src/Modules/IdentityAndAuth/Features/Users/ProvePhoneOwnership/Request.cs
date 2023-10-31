using Common.Core.Contracts.Results;
using NimbleMediator.Contracts;

namespace IdentityAndAuth.Features.Users.ProvePhoneOwnership;

public sealed record Request(string PhoneNumber, string Otp) : IRequest<Result<Response>>;
