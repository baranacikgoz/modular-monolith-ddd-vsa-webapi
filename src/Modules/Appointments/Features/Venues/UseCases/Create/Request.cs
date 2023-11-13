using Common.Core.Contracts.Results;
using NimbleMediator.Contracts;

namespace Appointments.Features.Venues.UseCases.Create;

public sealed record Request(string Name) : IRequest<Result<Response>>;
