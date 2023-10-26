using System.Net;
using Common.Core.Contracts.Results;

namespace Common.Core.Contracts.Errors;

public abstract record DomainError : Failure
{
    protected DomainError(HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
    }
}
