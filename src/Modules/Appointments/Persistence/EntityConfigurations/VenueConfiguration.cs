using Appointments.Features.Appointments.Domain;
using Appointments.Features.Venues.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appointments.Persistence.EntityConfigurations;

internal class VenueConfiguration : IEntityTypeConfiguration<Venue>
{
    public void Configure(EntityTypeBuilder<Venue> builder)
    {
        builder
            .Property(v => v.Id)
            .HasConversion(
                id => id.Value,
                value => new VenueId(value));

        builder
            .Property(v => v.Name)
            .HasMaxLength(Constants.NameMaxLength)
            .IsRequired();

        builder
            .ComplexProperty(v => v.Coordinates, c =>
            {
                c.Property(c => c.Latitude).IsRequired();
                c.Property(c => c.Longitude).IsRequired();
            });

        builder
            .HasMany(v => v.Appointments)
            .WithOne(a => a.Venue)
            .HasForeignKey(a => a.VenueId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
