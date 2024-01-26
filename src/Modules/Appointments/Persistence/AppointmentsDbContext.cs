using Appointments.Features.Appointments.Domain;
using Appointments.Features.Venues.Domain;
using Common.Core.Auth;
using Common.Eventbus;
using Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Appointments.Persistence;

internal sealed class AppointmentsDbContext(
    DbContextOptions<AppointmentsDbContext> options,
    ICurrentUser currentUser,
    IEventBus eventBus,
    ILogger<AppointmentsDbContext> logger
    ) : BaseDbContext(options, currentUser, eventBus, logger)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(nameof(Appointments));
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppointmentsDbContext).Assembly);
    }

    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<Venue> Venues => Set<Venue>();
}
