using Appointments.ModuleSetup.RateLimiting;
using Common.Core.Auth;
using Common.Core.Contracts.Results;
using Common.Core.EndpointFilters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NimbleMediator.Contracts;

namespace Appointments.Features.Appointments.UseCases.Book;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder appointmentsApiGroup)
    {
        appointmentsApiGroup
            .MapPost("", BookAppointmentAsync)
            .WithDescription("Book an appointment.")
            .RequireRateLimiting(RateLimitingConstants.BookAppointmentConcurrency)
            .MustHavePermission(RfActions.Create, RfResources.Appointments)
            .Produces<Response>(StatusCodes.Status200OK)
            .TransformResultTo<Response>();
    }

    // Route param names must be the same as the ones defined above.
    private static ValueTask<Result<Response>> BookAppointmentAsync(
        [FromRoute] Guid venueId,
        [FromRoute] DateTime appointmentDate,
        [FromBody] Request request,
        CancellationToken cancellationToken)
        => throw new NotImplementedException();
}
