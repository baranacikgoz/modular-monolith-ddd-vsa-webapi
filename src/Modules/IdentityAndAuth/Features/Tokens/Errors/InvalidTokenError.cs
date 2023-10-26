using Common.Core.Contracts.Results;

namespace IdentityAndAuth.Features.Tokens.Errors;

public sealed record InvalidTokenError() : Failure;
