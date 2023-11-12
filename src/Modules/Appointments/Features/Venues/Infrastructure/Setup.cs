using Appointments.Features.Venues.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Appointments.Features.Venues.Infrastructure;

internal static class Setup
{
    public static IServiceCollection AddVenuesInfrastructure(this IServiceCollection services)
        => services
            .AddScoped<IDummyVenueService, DummyVenueService>();
}
