using Common.Core.Contracts.Results;
using NimbleMediator.Contracts;

namespace IdentityAndAuth.Features.Users.Current.Get;

public sealed record Request() : IRequest<Result<Response>>;
