using Common.Application.ValueConverters;
using Common.Domain.Entities;
using Common.Domain.StronglyTypedIds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.Application.Persistence.EntityConfigurations;

public abstract class AuditableEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : class, IAuditableEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder
            .Property(e => e.CreatedOn)
            .IsRequired();

        builder
            .Property(e => e.CreatedBy)
            .HasConversion<StronglyTypedIdValueConverter<ApplicationUserId>>()
            .IsRequired(false);

        builder
            .Property(e => e.LastModifiedOn)
            .IsRequired(false);

        builder
            .Property(e => e.LastModifiedBy)
            .HasConversion<StronglyTypedIdValueConverter<ApplicationUserId>>()
            .IsRequired(false);
    }
}

public abstract class AuditableEntityConfiguration<TEntity, TId> : AuditableEntityConfiguration<TEntity>
    where TId : IStronglyTypedId, new()
    where TEntity : AuditableEntity<TId>
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);

        builder.HasKey(e => e.Id);

        builder
            .Property(e => e.Id)
            .HasConversion<StronglyTypedIdValueConverter<TId>>()
            .IsRequired();
    }
}
