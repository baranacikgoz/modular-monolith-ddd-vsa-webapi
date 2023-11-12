using Common.Core.Contracts.Results;
using NimbleMediator.Contracts;

namespace IdentityAndAuth.Features.Identity.UseCases.Users.Current.Get;

public sealed record Request() : IRequest<Result<Response>>;
