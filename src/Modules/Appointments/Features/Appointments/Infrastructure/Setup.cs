using Appointments.Features.Appointments.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Appointments.Features.Appointments.Infrastructure;

internal static class Setup
{
    public static IServiceCollection AddAppointmentsInfrastructure(this IServiceCollection services)
        => services
            .AddScoped<IDummyAppointmentService, DummyAppointmentService>();
}
