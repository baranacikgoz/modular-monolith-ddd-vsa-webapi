using Common.Core.Contracts.Results;
using NimbleMediator.Contracts;

namespace IdentityAndAuth.Features.Identity.UseCases.Users.Get;

internal sealed record Request(Guid Id) : IRequest<Result<Response>>;
