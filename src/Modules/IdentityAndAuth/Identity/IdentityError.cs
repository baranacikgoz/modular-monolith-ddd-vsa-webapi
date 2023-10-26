using System.Net;
using Common.Core.Contracts.Results;

namespace IdentityAndAuth.Identity;

public sealed record IdentityError(IEnumerable<string> errors) : Failure(HttpStatusCode.BadRequest, errors);
