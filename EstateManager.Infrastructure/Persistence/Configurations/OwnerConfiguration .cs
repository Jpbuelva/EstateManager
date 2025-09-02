using EstateManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstateManager.Infrastructure.Persistence.Configurations;

public class OwnerConfiguration : IEntityTypeConfiguration<Owner>
{
    public void Configure(EntityTypeBuilder<Owner> builder)
    {
        builder.HasKey(o => o.IdOwner); 

        builder.Property(o => o.Name)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(o => o.Address)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(o => o.Photo)
               .HasMaxLength(500);

        builder.Property(o => o.Birthday)
               .IsRequired();

        // Relaciones
        builder.HasMany(o => o.Properties)
               .WithOne(p => p.Owner)
               .HasForeignKey(p => p.IdOwner);
    }
}
