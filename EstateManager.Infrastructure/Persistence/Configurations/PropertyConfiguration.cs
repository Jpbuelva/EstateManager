using EstateManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstateManager.Infrastructure.Persistence.Configurations;

public class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
               .ValueGeneratedOnAdd(); 

        builder.Property(p => p.Address)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(p => p.City)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(p => p.State)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(p => p.Price)
               .HasColumnType("decimal(18,2)");

        builder.Property(p => p.CreatedAt)
               .HasDefaultValueSql("GETUTCDATE()");
    }
}
