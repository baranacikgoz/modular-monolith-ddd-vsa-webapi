using Common.Core.Contracts.Results;
using NimbleMediator.Contracts;

namespace IdentityAndAuth.Features.Users.Get;

internal sealed record Request(Guid Id) : IRequest<Result<Response>>;
