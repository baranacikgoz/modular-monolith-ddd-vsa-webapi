using Appointments.Features.Appointments.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appointments.Persistence.EntityConfigurations;

internal class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder
            .Property(a => a.Id)
            .HasConversion(
                id => id.Value,
                value => new AppointmentId(value));

        builder
            .Property(a => a.UserId)
            .IsRequired();

        builder
            .Property(a => a.State)
            .IsRequired();

        /// Appointments having <see cref="AppointmentState.Completed"/> state will grow over time and outnumber
        /// other appointments those have different states such as <see cref="AppointmentState.Scheduled"/> or <see cref="AppointmentState.Booked"/>.
        /// So we have to have an index for state (otherwise query performance for uncompleted appointments will be horrible over time),
        /// or separate tables for each state which could be a maintenance nightmare.
        builder
            .HasIndex(a => a.State);

        builder
            .Property(a => a.VenueId)
            .IsRequired();

        builder
            .HasOne(a => a.Venue)
            .WithMany(v => v.Appointments)
            .HasForeignKey(a => a.VenueId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
