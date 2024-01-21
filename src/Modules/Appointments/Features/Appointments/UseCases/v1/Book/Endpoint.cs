using Appointments.ModuleSetup.RateLimiting;
using Common.Core.Auth;
using Common.Core.Contracts.Results;
using Common.Core.EndpointFilters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Appointments.Features.Appointments.UseCases.v1.Book;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder appointmentsApiGroup)
    {
        appointmentsApiGroup
            .MapPost("{venueId}/{appointmentDate}", BookAppointmentAsync)
            .WithDescription("Book an appointment.")
            .MustHavePermission(RfActions.Create, RfResources.Appointments)
            .RequireRateLimiting(RateLimitingConstants.BookAppointmentConcurrency)
            .Produces<Response>(StatusCodes.Status200OK)
            .TransformResultTo<Response>();
    }

    private static ValueTask<Result<Response>> BookAppointmentAsync(
        [FromRoute] Guid venueId,
        [FromRoute] string appointmentDate,
        [FromBody] Request request,
        CancellationToken cancellationToken)
        => throw new NotImplementedException();
}
