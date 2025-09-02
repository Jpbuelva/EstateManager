using EstateManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstateManager.Infrastructure.Persistence.Configurations;

public class PropertyTraceConfiguration : IEntityTypeConfiguration<PropertyTrace>
{
    public void Configure(EntityTypeBuilder<PropertyTrace> builder)
    {
        // PK
        builder.HasKey(t => t.IdPropertyTrace);

        builder.Property(t => t.DateSale)
               .IsRequired();

        builder.Property(t => t.Name)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(t => t.Value)
               .HasColumnType("decimal(18,2)");

        builder.Property(t => t.Tax)
               .HasColumnType("decimal(18,2)");

        // FK y relación
        builder.HasOne(t => t.Property)
               .WithMany(p => p.Traces)
               .HasForeignKey(t => t.IdProperty)
               .OnDelete(DeleteBehavior.Cascade);  
    }
}
