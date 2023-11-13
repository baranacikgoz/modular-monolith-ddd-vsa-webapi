using Common.Core.Contracts.Results;
using NimbleMediator.Contracts;

namespace Appointments.Features.Venues.UseCases.Create;

internal sealed class RequestHandler : IRequestHandler<Request, Result<Response>>
{
    public ValueTask<Result<Response>> HandleAsync(Request request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
