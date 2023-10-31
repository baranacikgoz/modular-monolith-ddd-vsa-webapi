using Common.Core.Contracts.Results;
using NimbleMediator.Contracts;

namespace IdentityAndAuth.Features.Tokens.Create;

public sealed record Request(string PhoneVerificationToken, string PhoneNumber) : IRequest<Result<Response>>;
