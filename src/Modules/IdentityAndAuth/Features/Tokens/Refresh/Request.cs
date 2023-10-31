using Common.Core.Contracts.Results;
using NimbleMediator.Contracts;

namespace IdentityAndAuth.Features.Tokens.Refresh;

public sealed record Request(string ExpiredAccessToken, string RefreshToken) : IRequest<Result<Response>>;
