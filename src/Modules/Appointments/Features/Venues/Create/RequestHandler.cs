using Common.Core.Auth;
using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using NimbleMediator.Contracts;

namespace Appointments.Features.Venues.Create;

internal sealed class RequestHandler : IRequestHandler<Request, Result<Response>>
{
    public ValueTask<Result<Response>> HandleAsync(Request request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
