using EstateManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstateManager.Infrastructure.Persistence.Configurations;

public class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> builder)
    {
        builder.HasKey(p => p.IdProperty);

        builder.Property(p => p.IdProperty)
               .ValueGeneratedOnAdd();

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(p => p.Address)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(p => p.Price)
               .HasColumnType("decimal(18,2)");

        builder.Property(p => p.Year)
               .IsRequired();

        builder.Property(p => p.IdOwner)
               .IsRequired();

        // Relaciones
        builder.HasMany(p => p.Images)
               .WithOne(i => i.Property)
               .HasForeignKey(i => i.IdProperty);

        builder.HasMany(p => p.Traces)
               .WithOne(t => t.Property)
               .HasForeignKey(t => t.IdProperty);
    }
}
